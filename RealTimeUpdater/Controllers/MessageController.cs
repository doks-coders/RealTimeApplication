using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeUpdater.Extensions;
using RealTimeUpdater.Helpers;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Models.Requests;

namespace RealTimeUpdater.Controllers
{
	[Authorize]
	public class MessageController : ParentController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MessageMapper _mapper;
		public MessageController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_mapper= new MessageMapper();
		}
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

		[HttpGet("Get-ChatMessages/{recieverId}")]
		public async Task<ActionResult> GetMessage(int recieverId)
		{
			var messages = await _unitOfWork.Messages.GetAll(u=>
			u.RecieverId==recieverId 
			||
			u.SenderId==User.GetUserId()
			|| //Both Sides
			u.SenderId == recieverId
			||
			u.RecieverId == User.GetUserId()
			);
			if (messages == null) return BadRequest("");
			messages = messages.OrderBy(e=>e.DateCreated);

			var res =  _mapper.MessageToMessageResponse(messages.ToList());
			
			return Ok(res);
		}
	}

}
