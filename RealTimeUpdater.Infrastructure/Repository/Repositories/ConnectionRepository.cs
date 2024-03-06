using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.Infrastructure.Repository.Repositories
{
	public class ConnectionRepository : Repository<Connection>, IConnectionRepository
	{
		public ConnectionRepository(ApplicationDbContext db) : base(db)
		{
		}
	}
}
