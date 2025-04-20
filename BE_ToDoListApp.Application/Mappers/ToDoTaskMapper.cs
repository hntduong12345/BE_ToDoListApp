using AutoMapper;
using BE_ToDoListApp.Application.DTOs.ToDoTaskDTO;
using BE_ToDoListApp.Application.Utils;
using BE_ToDoListApp.Domain.Entities;
using BE_ToDoListApp.Domain.Enums.ToDoTaskEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Mappers
{
    public class ToDoTaskMapper : Profile
    {
        public ToDoTaskMapper()
        {
            CreateMap<ToDoTaskDTO, ToDoTask>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.State == "Created" ? Guid.CreateVersion7() : src.Id))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => Enum.Parse<ToDoTaskStatusEnum>(src.Status)))
                .ForMember(des => des.Priority, opt => opt.MapFrom(src => Enum.Parse<ToDoTaskPriorityEnum>(src.Priority)));

            CreateMap<ToDoTask, ToDoTaskDTO>()
                .ForMember(des => des.State, opt => opt.MapFrom(src => "Current"))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => EnumUtil.GetEnumName<ToDoTaskStatusEnum>(src.Status)))
                .ForMember(des => des.Priority, opt => opt.MapFrom(src => EnumUtil.GetEnumName<ToDoTaskPriorityEnum>(src.Priority)));
        }
    }
}   
