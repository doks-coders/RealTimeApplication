using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.Infrastructure.Repository.Repositories
{
	public class UserRepository : Repository<ApplicationUser>, IUserRepository
	{
		public UserRepository(ApplicationDbContext db) : base(db)
		{
		}
	}
}
