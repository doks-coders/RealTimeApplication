using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.ApplicationCore.Services.Services;
using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Repositories;
using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Requests;
using System.Text.Json;

namespace NUnitTest.ApplicationCore
{
	[TestFixture]
	public class MessageServiceTests
	{
		private DbContextOptions<ApplicationDbContext> options;

		private IMessageService _messageService;

		[SetUp]
		public void Setup()
		{
			options = new DbContextOptionsBuilder<ApplicationDbContext>()
			   .UseInMemoryDatabase(databaseName: "temp_database").Options;

			using (var context = new ApplicationDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.Messages.AddRange(
				new List<Message>(){

				new Message(){
					Content="My name is Mary",
					DateCreated=DateTime.Now,
					DateRead=DateTime.Now,
					RecieverId=1,
					SenderId=2,
					Deleted=false
				},

				new Message(){
					Content="My name is Peter",
					DateCreated=DateTime.Now,
					DateRead=DateTime.Now,
					RecieverId=1,
					SenderId=2,
					Deleted=false
				},

				});

				context.SaveChanges();

			}

			_messageService = new MessageService(new UnitOfWork(
				new ApplicationDbContext(options)
				));

		}


		[TestCase(1, 2)]
		[TestCase(2, 1)]
		public async Task CheckIfMessagesMatch_InputTwoIds_TrueResponse(int recieverId, int userId)
		{
			var res = await _messageService.GetMessages(recieverId, userId);

			var matchesParams = res.All(e =>
			e.RecieverId == recieverId && e.SenderId == userId
			|| e.SenderId == recieverId && e.RecieverId == userId);

			Assert.IsTrue(matchesParams);

		}

		[TestCase(3, 5)]
		public async Task AddMessageToDatabase(int recieverId, int userId)
		{
			var messageRequest = new MessageRequest()
			{
				Content = "I am a man",
				DateCreated = DateTime.Now,
				RecieverId = recieverId
			};
			await _messageService.SendMessage(messageRequest, userId);

			using (var context = new ApplicationDbContext(options))
			{
				var item = await context.Messages.FirstOrDefaultAsync(e => e.SenderId == userId && e.RecieverId == recieverId);
				Assert.IsNotNull(item);
				Assert.AreEqual(item.RecieverId, recieverId);
			}


		}
	}


}