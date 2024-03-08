using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;

namespace RealTimeUpdater.Infrastructure.Repository.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public IMessageRepository Messages { get; }

		public IUserRepository Users { get; }

		public IGroupRepository Groups { get; }

		public IConnectionRepository Connections { get; }

		private readonly ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
		{
			Messages = new MessageRepository(db);
			Users = new UserRepository(db);
			Groups = new GroupRepository(db);
			Connections = new ConnectionRepository(db);
			_db = db;
		}

		public async Task<bool> Save()
		{
			return 0 < await _db.SaveChangesAsync();
		}
	}
}
