using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RealTimeUpdater.SignalR
{
	public class UpdatesHub : Hub
	{
		private readonly PresenceTracker _tracker;

		public UpdatesHub(PresenceTracker tracker)
		{
			_tracker = tracker;
		}
		private List<int> DataValues()
		{
			return Enumerable.Range(0, 1000).ToList();
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


		public override async Task OnConnectedAsync()
		{
			_tracker.SetConnectedUser("admin@gmail.com", Context.ConnectionId);

			var users = _tracker.ConnectionsMade;

			if (users.Count() == 1)
			{
				List<List<int>> data = GetNumbers();
				for (int i = 0; i < data.Count; i++)
				{
					await Clients.All.SendAsync("UpdateInformation", new { array =  JsonSerializer.Serialize(data[i]) });
					await Task.Delay(800);
				}
			}



			//Group group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);

			//string groupName = GetGroupName(sender.UserName, recipient.UserName);
			//Group group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);


			/*
			

			In order to send a message between two people they must belong in a group

			Group group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);
			if (group.Connections.Any(u => u.Username == recipient.UserName)) 
			{ 
				message.DateRead = DateTime.UtcNow;
			}



			 List<string> connections = await PresenceTracker.GetConnectionForUser(recipient.UserName);
				if(connections != null)
				{
			If there you have a  connection stored to the hubConnection
			It will send this event directly to you.

			Prequisites you need a data table of your connections, with the
			
					await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", new { 
						username = sender.UserName, knownAs = sender.KnownAs 
					});

				}
			 */



			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await base.OnDisconnectedAsync(exception);
		}


	}
}
