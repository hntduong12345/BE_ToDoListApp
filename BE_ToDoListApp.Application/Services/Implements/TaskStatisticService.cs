using AutoMapper;
using BE_ToDoListApp.Application.Interfaces;
using BE_ToDoListApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Services.Implements
{
    public class TaskStatisticService : BaseService<TaskStatisticService>, ITaskStatisticService
    {
        public TaskStatisticService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        //Get today statistic


        //Get Weekly Statistic
    }
}
