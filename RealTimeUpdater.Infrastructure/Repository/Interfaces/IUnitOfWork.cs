namespace RealTimeUpdater.Infrastructure.Repository.Interfaces
{
	/// <summary>
	/// This is our unit of work
	/// </summary>
	public interface IUnitOfWork
	{
		IMessageRepository Messages { get; }
		IUserRepository Users { get; }
		IGroupRepository Groups { get; }
		IConnectionRepository Connections { get; }

		Task<bool> Save();
	}
}
