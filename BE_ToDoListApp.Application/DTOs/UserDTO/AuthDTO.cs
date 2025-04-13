using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.DTOs.UserDTO
{
    public record AuthDTO(
        string Token,
        string userId);

    public record SignInDTO(
        [Required] string Email,
        [Required] string Password
        );

    public record SignUpDTO(
        [Required] string FirstName,
        [Required] string LastName,
        string UserName,
        [Required] string Email,
        [Required] string Password
        );
}
