using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE_ToDoListApp.Domain.Entities
{
    public class ToDoTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Objective { get; set; }
        public string? Descrption { get; set; }
        public byte Priority { get; set; }
        public byte Status { get; set; }
        public DateOnly CreateDate { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
