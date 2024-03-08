using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using System.Linq.Expressions;

namespace RealTimeUpdater.Infrastructure.Repository.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		private readonly DbSet<T> _dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			_dbSet = _db.Set<T>();
		}


		public async Task<T?> Get(Expression<Func<T, bool>>? expression, string? includeProperties = null)
		{
			IQueryable<T> query = _dbSet.AsQueryable();

			if (includeProperties != null)
			{
				query = IncludeProperty(includeProperties, query);
			}

			var res = await query.FirstOrDefaultAsync(expression);
			return res;
		}



		public async Task<IEnumerable<T?>> GetAll(Expression<Func<T, bool>>? expression = null, string? includeProperties = null)
		{
			IQueryable<T> query = _dbSet.AsQueryable();

			if (includeProperties != null)
			{
				query = IncludeProperty(includeProperties, query);
			}
			List<T> res = (expression != null) ?
				await query.Where(expression).ToListAsync() :
				await query.ToListAsync();


			return res;
		}

		public async Task Add(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task AddRange(IEnumerable<T> entities)
		{
			await _dbSet.AddRangeAsync(entities);
		}


		public async Task Remove(T entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task RemoveRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}


		private IQueryable<T> IncludeProperty(string includeProperties, IQueryable<T> query)
		{
			string[] properties = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
			foreach (string property in properties)
			{
				query = query.Include(property);
			}
			return query;
		}
	}
}
