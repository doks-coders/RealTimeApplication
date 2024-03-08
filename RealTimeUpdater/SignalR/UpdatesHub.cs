using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace RealTimeUpdater.SignalR
{
	public class UpdatesHub : Hub
	{
		private readonly PresenceTracker _tracker;

		public UpdatesHub(PresenceTracker tracker)
		{
			_tracker = tracker;
		}

		private List<List<int>> GetNumbers()
		{
			List<List<int>> numbers = new();

			Random rnd = new Random();

			Enumerable.Range(0, 1000).ToList().ForEach((e) =>
			{
				numbers.Add(new List<int>() {
					rnd.Next(1, 10),
					rnd.Next(1, 10),
					rnd.Next(1, 10),
					rnd.Next(1, 10),
					rnd.Next(1, 10)
				});

			});

			return numbers;
		}

		/// <summary>
		/// Experimenting with graphs
		/// </summary>
		/// <returns></returns>
		public override async Task OnConnectedAsync()
		{
			_tracker.SetConnectedUser("admin@gmail.com", Context.ConnectionId);

			var users = _tracker.ConnectionsMade;

			if (users.Count() == 1)
			{
				List<List<int>> data = GetNumbers();
				for (int i = 0; i < data.Count; i++)
				{
					await Clients.All.SendAsync("UpdateInformation", new { array = JsonSerializer.Serialize(data[i]) });
					await Task.Delay(800);
				}
			}
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await base.OnDisconnectedAsync(exception);
		}


	}
}
