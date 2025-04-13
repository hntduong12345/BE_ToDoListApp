using BE_ToDoListApp.Application.DTOs.ToDoTaskDTO;
using BE_ToDoListApp.Application.Interfaces;
using BE_ToDoListApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Services.Interfaces
{
    public interface IToDoTaskService
    {
        Task<List<ToDoTaskDTO>> GetDateTasks(string userId, DateOnly date);
        Task ModifyTasks(List<ToDoTaskDTO> data, string userId, DateOnly date);
    }
}
