using Microsoft.AspNetCore.Mvc;

namespace RealTimeUpdater.Controllers
{
	public class MessageController : ParentController
	{
		public async Task<ActionResult> SendMessage()
		{
			return Ok();
		}

		public async Task<ActionResult> GetMessage()
		{
			return Ok();
		}
	}
}
