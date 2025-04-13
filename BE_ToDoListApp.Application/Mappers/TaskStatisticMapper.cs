using AutoMapper;
using BE_ToDoListApp.Application.DTOs.TaskStatisticDTO;
using BE_ToDoListApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Mappers
{
    public class TaskStatisticMapper : Profile
    {
        public TaskStatisticMapper()
        {
            CreateMap<TaskStatistic, ToDayTaskStatisticDTO>();

            CreateMap<TaskStatistic, WeekDaysStat>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.CalcDate.DayOfWeek.ToString("ddd")));
        }
    }
}
