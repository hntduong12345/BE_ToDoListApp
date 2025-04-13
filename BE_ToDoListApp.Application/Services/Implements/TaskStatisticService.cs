using AutoMapper;
using BE_ToDoListApp.Application.DTOs.TaskStatisticDTO;
using BE_ToDoListApp.Application.Interfaces;
using BE_ToDoListApp.Application.Services.Interfaces;
using BE_ToDoListApp.Application.Utils;
using BE_ToDoListApp.Domain.Entities;
using BE_ToDoListApp.SharedLibrary.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Services.Implements
{
    public class TaskStatisticService : BaseService<TaskStatisticService>, ITaskStatisticService
    {
        public TaskStatisticService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public async Task<ToDayTaskStatisticDTO> GetToDayStatistic(string userId)
        {
            Guid realUserId = HashUtil.DecryptId(userId);

            TaskStatistic stat = await _unitOfWork.GetRepository<TaskStatistic>()
                .GetAsync(predicate: t => t.UserId == realUserId && 
                                          t.CalcDate == DateOnly.FromDateTime(DateTime.Now));

            var res = _mapper.Map<ToDayTaskStatisticDTO>(stat);

            return res;
        }

        public async Task<WeeklyTaskStatisticDTO> GetWeeklyStatistic(string userId)
        {
            Guid realUserId = HashUtil.DecryptId(userId);

            List<DateOnly> weekDays = DaysUtil.GetWeekDays(DateOnly.FromDateTime(DateTime.Now));

            List<TaskStatistic> weeklyStats = (List<TaskStatistic>)await _unitOfWork.GetRepository<TaskStatistic>()
                .GetListAsync(predicate: t => t.CalcDate >= weekDays[0] &&
                                              t.CalcDate <= weekDays[6] &&
                                              t.UserId == realUserId);


            WeeklyStat totalStats = new WeeklyStat(weeklyStats.Average(t => t.Completed),
                                              weeklyStats.Average(t => t.InProgress),
                                              weeklyStats.Average(t => t.NotStarted));

            List<WeekDaysStat> weekDaysStats = _mapper.Map<List<WeekDaysStat>>(weeklyStats);

            return new WeeklyTaskStatisticDTO(totalStats, weekDaysStats);
        }

    }
}
