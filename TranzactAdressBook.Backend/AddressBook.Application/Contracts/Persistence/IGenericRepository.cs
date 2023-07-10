using AddressBook.Domain.Common;
using System.Linq.Expressions;

namespace AddressBook.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAllAync();
        Task<IReadOnlyList<T>> GetAync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAync(Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       string? includeString = null,
                                       bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAync(Expression<Func<T, bool>>? predicate = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                      List<Expression<Func<T, object>>>? includes = null,
                                      bool disableTracking = true);
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
