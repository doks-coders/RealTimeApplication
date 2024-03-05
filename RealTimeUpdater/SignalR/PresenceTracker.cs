namespace RealTimeUpdater.SignalR
{
	//Here we are going to create a presence tracker to track all the people using the sockets
	public class PresenceTracker
	{
		public Dictionary<string, List<string>> ConnectedUsers = new Dictionary<string, List<string>>();

		public List<string> ConnectionsMade = new();
		/// <summary>
		/// This method can be used for caching usernames and their connection ids
		/// </summary>
		/// <param name="username"></param>
		/// <param name="connectionId"></param>
		#region Adding User Method
		public void SetConnectedUser(string username, string connectionId)
		{
			ConnectionsMade.Add(username);
			lock (ConnectedUsers)
			{
				if (ConnectedUsers.ContainsKey(username))
				{
					ConnectedUsers[username].Add(connectionId);
				}
				else
				{
					ConnectedUsers.Add(username, new List<string>() { connectionId });
				}
			}
		}
		#endregion

		/// <summary>
		/// This method can be used for removing the usernames and their connection ids from the cache
		/// </summary>
		/// <param name="username"></param>
		/// <param name="connectionId"></param>
		#region Remove User Method
		public void RemoveDisconnectedUser(string username, string connectionId)
		{
			lock (ConnectedUsers)
			{
				if (ConnectedUsers.ContainsKey(username))
				{
					ConnectedUsers[username].Remove(connectionId);
					if (ConnectedUsers[username].Count() == 0)
					{
						ConnectedUsers.Remove(username);
					}
				}
			}
		}
		#endregion


		public Task<List<string>> GetUsers()
		{
			List<string> users = new();
			lock (ConnectedUsers)
			{
				users = ConnectedUsers.OrderBy(k => k.Key).Select(i => i.Key).ToList();
			}
			return Task.FromResult(users);
		}

		public Task GetUserConnections(string username)
		{
			List<string> connectionIds;
			lock (ConnectedUsers)
			{
				connectionIds = ConnectedUsers.GetValueOrDefault(username);
			}
			return Task.FromResult(connectionIds);
		}


	}
}
