using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.Infrastructure.Repository.Repositories
{
	public class MessageRepository : Repository<Message>, IMessageRepository
	{
		public MessageRepository(ApplicationDbContext db) : base(db) { }
	}
}
