using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.DTOs.ToDoTaskDTO
{
    public record ToDoTaskDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Objective { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public DateOnly CreateDate { get; set; }
        public string? State { get; set; }
    };
}
