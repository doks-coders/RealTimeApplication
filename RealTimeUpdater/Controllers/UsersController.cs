using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Response;

namespace RealTimeUpdater.Controllers
{
	[Authorize]
	public class UsersController : ParentController
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UsersController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		[HttpGet("all-users")]
		public async Task<ActionResult> AllUsers()
		{
			return Ok(await _userManager.Users.Where(e => e.Email != null).Select(e => new UserResponse(e.Email, e.Id)).ToListAsync());
		}
	}
}
