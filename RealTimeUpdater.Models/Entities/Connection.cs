namespace RealTimeUpdater.Models.Entities
{
	public class Connection
	{
		public int Id { get; set; }
		public string ConnectionId { get; set; }
		public string UserName { get; set; }

		public Connection(string userName, string connectionId)
		{
			UserName = userName;
			ConnectionId = connectionId;
		}
	}
}
