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
		public MessageController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[HttpPost("Send-Message")]
		public async Task<ActionResult> SendMessage([FromBody] MessageRequest messageRequest)
		{
			var mapper = new MessageMapper();
			var message = mapper.MessageRequestToMessage(messageRequest);
			message.SenderId = User.GetUserId();
			await _unitOfWork.Messages.Add(message);
			if (await _unitOfWork.Save())
			{
				return Ok();
			}
			return BadRequest("There was a problem with the saving");


		}

		[HttpGet("Get-Message")]
		public async Task<ActionResult> GetMessage()
		{
			var res = await _unitOfWork.Messages.GetAll();
			if (res == null) return BadRequest("");
			return Ok(res);
		}
	}

}
