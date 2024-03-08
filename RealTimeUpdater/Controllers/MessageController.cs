using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeUpdater.ApplicationCore.Helpers;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.Extensions;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;

namespace RealTimeUpdater.Controllers
{
	[Authorize]
	public class MessageController : ParentController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MessageMapper _mapper;
		private readonly IMessageService _messageService;
		public MessageController(IUnitOfWork unitOfWork, IMessageService messageService)
		{
			_unitOfWork = unitOfWork;
			_mapper = new MessageMapper();
			_messageService = messageService;
		}

		[HttpGet("get-mail-message")]
		public async Task<ActionResult> GetMailMessage([FromQuery] string mode)
		{
			var user = await _unitOfWork.Users.Get(u => u.Id == User.GetUserId(), includeProperties: "InboxMessages,OutBoxMessages");
			var messages = mode switch
			{
				"inbox" => user.InboxMessages,
				"outbox" => user.OutBoxMessages,
				_ => (await _unitOfWork.Messages.GetAll(u => u.RecieverId == User.GetUserId() || u.SenderId == User.GetUserId())).ToList()
			}; ;
			var res = _mapper.MessageToMessageResponse(messages.OrderByDescending(e => e.DateCreated).ToList());
			return Ok(res);
		}




		/*
		Send Message Request: [Redundant]
		------------------------------------------
		[HttpPost("Send-Message")]
		public async Task<ActionResult> SendMessage([FromBody] MessageRequest messageRequest)
		{
			if (User.GetUserId() == messageRequest.RecieverId) return BadRequest("Something is wrong");
			var message = _mapper.MessageRequestToMessage(messageRequest);
			message.SenderId = User.GetUserId();
			await _unitOfWork.Messages.Add(message);
			if (await _unitOfWork.Save())
			{
				return Ok();
			}
			return BadRequest("There was a problem with the saving");

		}
		*/


		/*
		Get All Chat Messages Request: [Redundant]
		-----------------------------------------
		[HttpGet("get-chatmessages/{recieverId}")]
		public async Task<ActionResult> GetMessage(int recieverId)
		{
			var res = await _messageService.GetMessages(recieverId, User.GetUserId());
			return Ok(res);
		}
		*/

	}

}