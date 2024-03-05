using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealTimeUpdater.Controllers
{
	[Authorize]
	public class MessageController : ParentController
	{
		[HttpGet("Send-Message")]
		public async Task<ActionResult> SendMessage()
		{
			return Ok("Accessed");
		}

		[HttpGet("Get-Message")]
		public async Task<ActionResult> GetMessage()
		{
			return Ok("Accessed");
		}
	}

}
