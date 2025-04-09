using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Interfaces
{
    public interface IUnitOfWork : IGenericRepositoryFactory, IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
