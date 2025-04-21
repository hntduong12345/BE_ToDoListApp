using BE_ToDoListApp.Application.DTOs.UserDTO;
using BE_ToDoListApp.Application.Services.Interfaces;
using BE_ToDoListApp.Presentation.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_ToDoListApp.Presentation.Controllers
{
    [Route(ApiEndpointConstant.ApiEndpoint)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet(ApiEndpointConstant.User.UserEndpoint)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo([FromRoute]string userId)
        {
            var res = await _userService.GetUserInfo(userId);
            return Ok(res);
        }

        [Authorize]
        [HttpPatch(ApiEndpointConstant.User.UserEndpoint)]
        public async Task<IActionResult> UpdateUserInfo([FromRoute]string userId, [FromBody]UserDTO data)
        {
            await _userService.UpdateUserInfo(userId, data);
            return Ok();
        }

        [Authorize]
        [HttpPatch(ApiEndpointConstant.User.UserPasswordEndpoint)]
        public async Task<IActionResult> UpdatePassword([FromRoute]string userId, [FromBody]string newPass)
        {
            await _userService.UpdatePassword(userId, newPass);
            return Ok();
        }
    }
}
