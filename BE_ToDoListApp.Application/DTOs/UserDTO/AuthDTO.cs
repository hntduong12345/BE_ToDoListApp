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

    public record SignUpDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public string? UserName { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required] 
        public string Password { get; set; } = null!;
    };
}
