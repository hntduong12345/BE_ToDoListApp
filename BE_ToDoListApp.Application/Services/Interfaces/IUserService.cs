using BE_ToDoListApp.Application.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Services.Interfaces
{
    public interface IUserService
    {
        #region User Functions
        Task<UserDTO> GetUserInfo(string userId);
        Task UpdateUserInfo(string userId, UserDTO updatedData);
        Task UpdatePassword(string userId, string newPass);
        #endregion

        #region Authorization
        Task<AuthDTO> SignIn(SignInDTO data);
        Task<AuthDTO> SignUp(SignUpDTO data);
        #endregion 
    }
}
