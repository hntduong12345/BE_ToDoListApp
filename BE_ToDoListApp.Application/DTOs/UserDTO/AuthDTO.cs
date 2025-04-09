using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.DTOs.UserDTO
{
    public record AuthDTO(
        string token);

    public record SignInDTO(
        [Required] string email,
        [Required] string password
        );

    public record SignUpDTO(
        [Required] string FirstName,
        [Required] string LastName,
        string UserName,
        [Required] string Email,
        [Required] string Password
        );
}
