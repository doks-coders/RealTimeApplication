using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Requests;
using RealTimeUpdater.Models.Response;

namespace RealTimeUpdater.Controllers
{
	public class AuthController : ParentController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITokenService _tokenService;
		public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_tokenService = tokenService;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegisterUserRequest registerUser)
		{
			var user = new ApplicationUser { UserName = registerUser.Email, Email = registerUser.Email };
			if (await _userManager.FindByEmailAsync(registerUser.Email) != null) return BadRequest("User Exists");

			var res = await _userManager.CreateAsync(user, registerUser.Password);
			if (res.Succeeded)
			{
				return Ok(new AuthUserResponse(
					Email: user.Email,
					Token: await _tokenService.CreateToken(user)
					));
			}
			return BadRequest();

		}

		[HttpPost("login")]
		public async Task<ActionResult> LoginUser([FromBody] LoginUserRequest loginUser)
		{

			var user = await _userManager.FindByEmailAsync(loginUser.Email);
			if (user == null) return BadRequest("User Not Found");
			var matches = await _userManager.CheckPasswordAsync(user, loginUser.Password);

			if (matches)
			{
				return Ok(new AuthUserResponse(
					Email: user.Email,
					Token: await _tokenService.CreateToken(user)
					));
			}
			return BadRequest("Passwords do not match");
		}
		public async Task CheckIfUser(string email)
		{
			await _userManager.Users.FirstOrDefaultAsync(e => e.UserName == email);
		}
	}
}
