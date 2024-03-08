using System.Linq.Expressions;

namespace RealTimeUpdater.Infrastructure.Repository.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<T?> Get(Expression<Func<T, bool>>? expression, string? includeProperties = null);

		Task<IEnumerable<T?>> GetAll(Expression<Func<T, bool>>? expression = null, string? includeProperties = null);

		Task Add(T entity);

		Task AddRange(IEnumerable<T> entities);

		Task Remove(T entity);

		Task RemoveRange(IEnumerable<T> entities);
	}
}
