using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;

namespace RealTimeUpdater.Infrastructure.Repository.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public MessageRepository Messages { get; }

		private readonly ApplicationDbContext _db;
		public UnitOfWork(ApplicationDbContext db)
		{
			Messages = new MessageRepository(db);
			_db = db;
		}

		public async Task<bool> Save()
		{
			return 0 < await _db.SaveChangesAsync();
		}
	}
}
