using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.DTOs.TaskStatisticDTO
{
    public record ToDayTaskStatisticDTO(
        decimal Completed,
        decimal InProgress,
        decimal NotStarted
        );
}
