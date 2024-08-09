using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace DigitalStore.Data.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> predicate,Func<IQueryable<T>,IIncludableQueryable<T,object>>? include = null);
    Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    IQueryable<T> GetAllFilter();
    Task<bool> UpdatePartialAsync(T currententity,T updatedentity);
}