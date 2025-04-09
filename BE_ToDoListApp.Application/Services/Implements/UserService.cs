using AutoMapper;
using BE_ToDoListApp.Application.DTOs.UserDTO;
using BE_ToDoListApp.Application.Interfaces;
using BE_ToDoListApp.Application.Services.Interfaces;
using BE_ToDoListApp.Application.Utils;
using BE_ToDoListApp.Domain.Entities;
using BE_ToDoListApp.SharedLibrary.Utils;
using Microsoft.AspNetCore.Http;

namespace BE_ToDoListApp.Application.Services.Implements
{
    public class UserService : BaseService<UserService>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        #region Authentication
        public async Task<AuthDTO> SignIn(SignInDTO data)
        {
            User user = await _unitOfWork.GetRepository<User>().GetAsync(
            predicate: u => u.Email == data.email);
            if (user == null) throw new UnauthorizedAccessException("Email is not found");

            if (!HashUtil.VerifyPassword(data.password, user.Password, out var rehashedPassword))
                throw new UnauthorizedAccessException("Invalid Email or Password");

            if (rehashedPassword != null)
            {
                user.Password = rehashedPassword;
                _unitOfWork.GetRepository<User>().UpdateAsync(user);
                bool isSuccess = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccess) throw new Exception("An error occured when executing sign-in");
            }

            string token = JwtUtil.GenerateJwtToken(user);

            return new AuthDTO(token);
        }

        public async Task<AuthDTO> SignUp(SignUpDTO data)
        {
            User user = await _unitOfWork.GetRepository<User>().GetAsync(
                predicate: u => u.Email == data.Email || u.UserName == data.UserName);

            if (user != null) throw new BadHttpRequestException("Email or UserName has already existed");

            User createdUser = _mapper.Map<User>(data);
            await _unitOfWork.GetRepository<User>().InsertAsync(createdUser);
            bool isSuccess = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccess) throw new Exception("Error occured when executing sign-up");

            string token = JwtUtil.GenerateJwtToken(createdUser);

            return new AuthDTO(token);
        }
        #endregion
    }
}
