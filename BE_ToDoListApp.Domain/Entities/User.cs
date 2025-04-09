using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Domain.Entities
{
    public class User
    {
        public User()
        {
            ToDoTasks = new HashSet<ToDoTask>();
            TaskStatistics = new HashSet<TaskStatistic>();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public byte Role { get; set; }
        public byte Status { get; set; }

        public virtual ICollection<ToDoTask> ToDoTasks { get; set; }
        public virtual ICollection<TaskStatistic> TaskStatistics { get; set; }
    }
}
