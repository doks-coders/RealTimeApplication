namespace RealTimeUpdater.Infrastructure.Repository.Interfaces
{
	public interface IUnitOfWork
	{
		IMessageRepository Messages { get; }
		IUserRepository Users { get; }
		IGroupRepository Groups { get; }
		IConnectionRepository Connections { get; }

		Task<bool> Save();
	}
}
