namespace RealTimeUpdater.Models.Entities
{
	/// <summary>
	/// This is the connection entity. It gets the connections from our Hub in SignalR
	/// </summary>
	public class Connection
	{
		public int Id { get; set; }
		public string ConnectionId { get; set; }
		public string UserName { get; set; }
		public string GroupName { get; set; }
		public Group Group { get; set; }

		public Connection(string userName, string connectionId, string groupName)
		{
			UserName = userName;
			ConnectionId = connectionId;
			GroupName = groupName;
		}

	}
}
