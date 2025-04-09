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
    public class ToDoTaskService : BaseService<ToDoTaskService>, IToDoTaskService
    {
        public ToDoTaskService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        //Get Tasks
        //public async Task<>

        //Modify Tasks

    }
}
