using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.Infrastructure.Repository.Repositories
{
	public class GroupRepository : Repository<Group>, IGroupRepository
	{
		public GroupRepository(ApplicationDbContext db) : base(db)
		{
		}
	}
}
