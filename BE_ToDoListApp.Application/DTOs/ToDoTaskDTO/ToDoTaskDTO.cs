using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.DTOs.ToDoTaskDTO
{
    public record ToDoTaskDTO(
        Guid Id,
        string Title,
        string Objective,
        string Description,
        string Priority,
        string Status,
        DateOnly CreateDate,
        string State
        );
}
