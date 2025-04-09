using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE_ToDoListApp.Domain.Entities
{
    public class TaskStatistic
    {
        public Guid Id { get; set; }
        public DateOnly CalcDate { get; set; }
        public decimal Completed { get; set; }
        public decimal InProgress { get; set; }
        public decimal NotStarted { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
