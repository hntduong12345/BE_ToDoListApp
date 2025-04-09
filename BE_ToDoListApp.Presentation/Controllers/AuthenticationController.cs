using BE_ToDoListApp.Application.DTOs.UserDTO;
using BE_ToDoListApp.Application.Services.Interfaces;
using BE_ToDoListApp.Presentation.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_ToDoListApp.Presentation.Controllers
{
    [Route(ApiEndpointConstant.ApiEndpoint)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(ApiEndpointConstant.Authentication.SignInEndpoint)]
        [ProducesResponseType(typeof(AuthDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignIn(SignInDTO data)
        {
            var res = await _userService.SignIn(data);
            return Ok(res);
        }

        [HttpPost(ApiEndpointConstant.Authentication.SignUpEndpoint)]
        [ProducesResponseType(typeof(AuthDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUp(SignUpDTO data)
        {
            var res = await _userService.SignUp(data);
            return Ok(res);
        }
    }
}
