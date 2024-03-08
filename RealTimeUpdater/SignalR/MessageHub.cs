using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.ApplicationCore.Helpers;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.Extensions;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Requests;

namespace RealTimeUpdater.SignalR
{
	public class MessageHub : Hub
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMessageService _messageService;
		private readonly MessageMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		public MessageHub(IUnitOfWork unitOfWork, IMessageService messageService, UserManager<ApplicationUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_messageService = messageService;
			_userManager = userManager;
			_mapper = new MessageMapper();
		}

		/// <summary>
		/// Creating and Maintaining a connection between two users i.e Reciever and User
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HubException"></exception>
		public override async Task<Task> OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();

			string? RecieverId = httpContext?.Request.Query["RecieverId"];

			if (RecieverId == null) throw new HubException("No User");
			var RecieverUser = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == int.Parse(RecieverId));

			string GroupName = GetGroupName(Context.User.GetUserName(), RecieverUser.UserName);

			await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);

			var messages = await _messageService.GetMessages(RecieverUser.Id, Context.User.GetUserId());

			await Clients.Group(GroupName).SendAsync("UserMessages", messages);

			await AddConnectionToGroup(Context.User.GetUserName(), RecieverUser.UserName);

			return base.OnConnectedAsync();
		}

		/// <summary>
		/// Sending messages between between two users
		/// </summary>
		/// <param name="messageRequest"></param>
		/// <returns></returns>
		/// <exception cref="HubException"></exception>
		public async Task SendMessage(MessageRequest messageRequest)
		{
			if (Context.User.GetUserId() == messageRequest.RecieverId) throw new HubException("Something is wrong");

			var recipientUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == messageRequest.RecieverId);
			var response = _mapper.MessageRequestToRespone(messageRequest);

			response.SenderId = Context.User.GetUserId();

			var GroupName = GetGroupName(Context.User.GetUserName(), recipientUser.UserName);

			var group = await _unitOfWork.Groups.Get(u => u.Name == GroupName);


			//---Putting an event here that should be used for notifications---//


			await Clients.Group(GroupName).SendAsync("NewMessage", response);

			await _messageService.SendMessage(messageRequest, Context.User.GetUserId());
		}

		/// <summary>
		/// This is for getting the groupName from the connection id
		/// </summary>
		/// <param name="connectionId"></param>
		/// <returns></returns>
		public async Task<string> GetGroupWithConnectionId(string connectionId)
		{
			var connection = await _unitOfWork.Connections.Get(u => u.ConnectionId == connectionId);
			return connection.GroupName;
		}

		public override async Task<Task> OnDisconnectedAsync(Exception? exception)
		{
			var groupName = await GetGroupWithConnectionId(Context.ConnectionId);
			if (groupName != null)
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
			}

			return base.OnDisconnectedAsync(exception);
		}




		/// <summary>
		/// This is for creating a group in the database and adding connections to it
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="recieverName"></param>
		/// <returns></returns>
		private async Task AddConnectionToGroup(string userName, string recieverName)
		{
			string groupName = GetGroupName(userName, recieverName);
			Group? group = await _unitOfWork.Groups.Get(e => e.Name == groupName);
			if (group == null)
			{
				await _unitOfWork.Groups.Add(new Group { Name = groupName });
			}
			else
			{
				group.Connections.Add(new Connection(userName, Context.ConnectionId, groupName));
			}
			await _unitOfWork.Save();

		}

		/// <summary>
		/// This for creating a groupname using two users names
		/// </summary>
		/// <param name="UserName"></param>
		/// <param name="RecieverName"></param>
		/// <returns></returns>
		private string GetGroupName(string UserName, string RecieverName)
		{
			int o = string.CompareOrdinal(UserName, RecieverName);

			if (o < 0)
			{
				return $"{RecieverName}-{UserName}";
			}

			return $"{UserName}-{RecieverName}";
		}

		public override string? ToString()
		{
			return base.ToString();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
