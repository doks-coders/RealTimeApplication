using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.ApplicationCore.Services.Services;
using RealTimeUpdater.Controllers;
using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Requests;

namespace NUnitTest.RealtimeUpdater.Controllers
{
	[TestFixture]
	public class AuthControllerTests
	{
		private AuthController _authController;
		private Mock<UserManager<ApplicationUser>> _userManager;
		private ITokenService _tokenService;
		private LoginUserRequest loginUser = new("user@mail.com", "Password1234");
		private RegisterUserRequest registerUserRequest = new("user@mail.com", "Password1234");


		[SetUp]
		public void Setup()
		{
			_userManager = new Mock<UserManager<ApplicationUser>>(
			Mock.Of<IUserStore<ApplicationUser>>(),
			null, null, null, null, null, null, null, null);

			var configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.Development.json")
			.Build();

			_tokenService = new TokenService(configuration, _userManager.Object);
			_authController = new(_userManager.Object, _tokenService);

		}

		[Test]
		public async Task UserAlreadyExists_ReturnsFails()
		{
			ApplicationUser alreadyExistingUser = new ApplicationUser() { UserName = "user@mail.com", Email = "user@mail.com" };
			_userManager.Setup(e => e.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(alreadyExistingUser);

			var res = (ObjectResult)await _authController.Register(registerUserRequest);

			Assert.AreEqual(res.StatusCode, 400);
			Assert.AreEqual(res.Value, "User Exists");

		}

		[Test]
		public async Task UserRegistered_ReturnsSucessfully()
		{
			_userManager.Setup(e => e.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
			_userManager.Setup(e => e.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

			var res = (ObjectResult)await _authController.Register(registerUserRequest);

			Assert.AreEqual(res.StatusCode, 200);

		}


		[Test]
		public async Task LoginUser_FoundUser_MatchesPassword_Successfully()
		{
			ApplicationUser registeredUser = new()
			{
				Email = loginUser.Email,
				UserName = loginUser.Email
			};
			_userManager.Setup(e => e.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(registeredUser);
			_userManager.Setup(e => e.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);

			var res = (ObjectResult)await _authController.LoginUser(loginUser);

			Assert.AreEqual(res.StatusCode, 200);

		}



		[Test]
		public async Task LoginUser_FoundUser_FailedPassword_ReturnsFails()
		{
			ApplicationUser registeredUser = new()
			{
				Email = loginUser.Email,
				UserName = loginUser.Email
			};
			_userManager.Setup(e => e.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(registeredUser);
			_userManager.Setup(e => e.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(false);

			var res = (ObjectResult)await _authController.LoginUser(loginUser);

			Assert.AreEqual(res.StatusCode, 400);
		}

		[Test]
		public async Task LoginUser_UserNotExist_ReturnsFails()
		{
			ApplicationUser registeredUser = new()
			{
				Email = loginUser.Email,
				UserName = loginUser.Email
			};
			_userManager.Setup(e => e.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);

			var res = (ObjectResult)await _authController.LoginUser(loginUser);

			Assert.AreEqual(res.StatusCode, 400);
		}
	}
}
