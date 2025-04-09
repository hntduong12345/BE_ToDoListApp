using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.SharedLibrary.Responses
{
    public record ErrorResponse(
        int StatusCode,
        string Error,
        DateTime TimeStamp
        );
}
