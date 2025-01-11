using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.SharedLibrary.Responses;

namespace ToDoApp.SharedLibrary.Interfaces
{
    public interface IGenericInterface<T> : IDisposable where T : class
    {
        #region Get Functions
        Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<TResult> SingleOrDefaultAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<ICollection<T>> GetListAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<ICollection<TResult>> GetListAsync<TResult>(
            Expression<Func<T, TResult>> selector = null,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        #endregion

        #region Insert/Create Functions
        Task<Response> InsertAsync(T entity);
        Task<Response> InsertManyAsync(IEnumerable<T> entities);
        #endregion

        #region Update Functions
        Task<Response> UpdateAsync(T entity);
        Task<Response> UpdateManyAsync(IEnumerable<T> entities);
        #endregion

        #region Delete Functions
        Task<Response> DeleteAsync(T entity);
        Task<Response> DeleteManyAsync(IEnumerable<T> entities);
        #endregion
    }
}
