using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        #region Get Functions
        Task<T> GetAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<TResult> GetAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<ICollection<T>> GetListAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<ICollection<TResult>> GetListAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        #endregion

        #region Insert (Create) Functions
        Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);
        #endregion

        #region Update Functions
        void UpdateAsync(T entity);

        void UpdateRangeAsync(IEnumerable<T> entities);
        #endregion

        #region Delete Functions
        void DeleteAsync(T entity);
        void DeleteRangeAsync(IEnumerable<T> entities);
        #endregion 
    }
}
