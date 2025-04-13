using BE_ToDoListApp.Application.DTOs.TaskStatisticDTO;
using BE_ToDoListApp.Application.Services.Interfaces;
using BE_ToDoListApp.Presentation.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_ToDoListApp.Presentation.Controllers
{
    [Route(ApiEndpointConstant.ApiEndpoint)]
    [Authorize]
    [ApiController]
    public class TaskStatisticController : ControllerBase
    {
        private readonly ITaskStatisticService _taskStatisticService;

        public TaskStatisticController(ITaskStatisticService taskStatisticService)
        {
            _taskStatisticService = taskStatisticService;
        }

        [HttpGet(ApiEndpointConstant.TaskStatistic.TodayStatEndpoint)]
        [ProducesResponseType(typeof(ToDayTaskStatisticDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodayStatistic([FromRoute] string userId)
        {
            var res = await _taskStatisticService.GetToDayStatistic(userId);

            return Ok(res);
        }

        [HttpGet(ApiEndpointConstant.TaskStatistic.WeekStatEndpoint)]
        [ProducesResponseType(typeof(WeeklyTaskStatisticDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeekStatistic([FromRoute] string userId)
        {
            var res = await _taskStatisticService.GetWeeklyStatistic(userId);

            return Ok(res);
        }
    }
}
