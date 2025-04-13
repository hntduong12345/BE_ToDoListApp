using BE_ToDoListApp.Application.DTOs.TaskStatisticDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Services.Interfaces
{
    public interface ITaskStatisticService
    {
        Task<ToDayTaskStatisticDTO> GetToDayStatistic(string userId);
        Task<WeeklyTaskStatisticDTO> GetWeeklyStatistic(string userId);
    }
}
