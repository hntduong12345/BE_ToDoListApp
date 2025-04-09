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
        Task<AuthDTO> SignIn(SignInDTO data);
        Task<AuthDTO> SignUp(SignUpDTO data);
    }
}
