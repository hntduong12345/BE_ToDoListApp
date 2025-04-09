using AutoMapper;
using BE_ToDoListApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected string GetUsernameFromJwt()
        {
            string username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return username;
        }

        protected string GetRoleFromJwt()
        {
            string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role)!;
            return role;
        }
    }
}
