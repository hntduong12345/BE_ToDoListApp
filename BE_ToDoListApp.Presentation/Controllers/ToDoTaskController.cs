using BE_ToDoListApp.Application.DTOs.ToDoTaskDTO;
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
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskService _toDoTaskService;

        public ToDoTaskController(IToDoTaskService toDoTaskService)
        {
            _toDoTaskService = toDoTaskService;
        }

        [Authorize]
        [HttpGet(ApiEndpointConstant.ToDoTask.ToDoTasksEndpoint)]
        [ProducesResponseType(typeof(List<ToDoTaskDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetToDoTasks([FromRoute] string userId, [FromQuery]DateOnly date)
        {
            var res = await _toDoTaskService.GetDateTasks(userId, date);
            return Ok(res);
        }

        [Authorize]
        [HttpPut(ApiEndpointConstant.ToDoTask.ToDoTasksEndpoint)]
        public async Task<IActionResult> ModifyTasks([FromRoute] string userId, [FromQuery] DateOnly date, [FromBody]List<ToDoTaskDTO> data)
        {
            await _toDoTaskService.ModifyTasks(data, userId, date);
            return Ok("Action Success");
        }
    }
}
