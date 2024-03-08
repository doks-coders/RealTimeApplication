using Microsoft.AspNetCore.Identity;
using Moq;
using RealTimeUpdater.Controllers;
using RealTimeUpdater.Models.Entities;

namespace NUnitTest.RealtimeUpdater.Controllers
{
	[TestFixture]
	public class UserControllerTests
	{
		private UsersController _usersController;
		private Mock<UserManager<ApplicationUser>> _userManager;

		[SetUp]
		public void Setup()
		{
			_userManager = new Mock<UserManager<ApplicationUser>>();

			_usersController = new UsersController(_userManager.Object);
		}

		[Test]
		public void GetAllUsersWithoutEmailTest()
		{

		}
	}
}
