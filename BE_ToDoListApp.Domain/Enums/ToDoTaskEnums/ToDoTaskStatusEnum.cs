using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Domain.Enums.ToDoTaskEnums
{
    public enum ToDoTaskStatusEnum : byte
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3,
    }
}
