using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.Extensions;
using RealTimeUpdater.Helpers;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Requests;
using RealTimeUpdater.Models.Response;

namespace RealTimeUpdater.SignalR
{
	public class MessageHub : Hub
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMessageService _messageService;
		private readonly MessageMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		public MessageHub(IUnitOfWork unitOfWork,IMessageService messageService,UserManager<ApplicationUser> userManager, MessageMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_messageService = messageService;
			_userManager = userManager;
			_mapper = mapper;
		}
		public override async Task<Task> OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();
			string RecieverName = httpContext.Request.Query["RecieverName"];
	
			string GroupName = GetGroupName(Context.User.GetUserName(), RecieverName);
			await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);

			var RecieverUser =  await _userManager.Users.FirstOrDefaultAsync(e=>e.UserName == RecieverName);
			var messages = await _messageService.GetMessages(RecieverUser.Id, Context.User.GetUserId());
			
			await Clients.Group(GroupName).SendAsync("UserMessages",messages);

			await AddConnectionToGroup(Context.User.GetUserName(), RecieverName);

			return base.OnConnectedAsync();
		}

		public async Task SendMessage(MessageRequest messageRequest)
		{
			if (Context.User.GetUserId() == messageRequest.RecieverId) throw new HubException("Something is wrong");

			var recipientUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == messageRequest.RecieverId);
			var response = _mapper.MessageRequestToRespone(messageRequest);

			response.SenderId = Context.User.GetUserId();

			var GroupName = GetGroupName(Context.User.GetUserName(), recipientUser.UserName);

			var group = await _unitOfWork.Groups.Get(u => u.Name == GroupName);
			

			//Putting an event here that should be used for notifications


			await Clients.Group(GroupName).SendAsync("NewMessage", response);

			await _messageService.SendMessage(messageRequest,Context.User.GetUserId());
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			return base.OnDisconnectedAsync(exception);
		}


	
		private async Task AddConnectionToGroup(string userName,string recieverName)
		{
			string groupName = GetGroupName(userName, recieverName);
			Group? group = await _unitOfWork.Groups.Get(e => e.Name == groupName);
			if(group == null)
			{
				await _unitOfWork.Groups.Add(new Group { Name = groupName });
			}
			else
			{
				group.Connections.Add(new Connection(userName, Context.ConnectionId));
			}
			await _unitOfWork.Save();

		}

		private string GetGroupName(string UserName, string RecieverName)
		{
			int o = string.CompareOrdinal(UserName, RecieverName);
			if (o == 0)
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
