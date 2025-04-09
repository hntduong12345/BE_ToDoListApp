using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.SharedLibrary.Responses
{
    public record Response(bool Flag = false, string Message = null!);
}
