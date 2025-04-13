using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.DTOs.TaskStatisticDTO
{
    public record WeeklyTaskStatisticDTO(
        WeeklyStat WeeklyStatistic,
        List<WeekDaysStat> WeekDaysStatistics
        );

    public record WeeklyStat(
        decimal Completed,
        decimal InProgress,
        decimal NotStarted
        );

    public record WeekDaysStat(
        string Name,
        decimal Completed,
        decimal InProgress,
        decimal NotStarted
        );
}
