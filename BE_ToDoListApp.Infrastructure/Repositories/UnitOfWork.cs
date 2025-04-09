using BE_ToDoListApp.Application.Interfaces;
using BE_ToDoListApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoListDBContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(ToDoListDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();
            if (_repositories.TryGetValue(typeof(TEntity), out object repository))
            {
                return (IGenericRepository<TEntity>)repository;
            }

            repository = new GenericRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return (IGenericRepository<TEntity>)repository;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public int Commit()
        {
            TrackChanges();
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            TrackChanges();
            return await _context.SaveChangesAsync();
        }

        private void TrackChanges()
        {
            var validationErrors = _context.ChangeTracker.Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(e => e != ValidationResult.Success)
                .ToArray();
            if (validationErrors.Any())
            {
                var exceptionMessage = string.Join(Environment.NewLine,
                    validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
                throw new Exception(exceptionMessage);
            }
        }
    }
}
