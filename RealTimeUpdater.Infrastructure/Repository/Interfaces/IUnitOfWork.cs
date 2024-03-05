using RealTimeUpdater.Infrastructure.Repository.Repositories;

namespace RealTimeUpdater.Infrastructure.Repository.Interfaces
{
	public interface IUnitOfWork
	{
		MessageRepository Messages { get; }

		Task<bool> Save();
	}
}
