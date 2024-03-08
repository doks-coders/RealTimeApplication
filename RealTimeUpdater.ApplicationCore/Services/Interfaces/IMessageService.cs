using RealTimeUpdater.Models.Requests;
using RealTimeUpdater.Models.Response;

namespace RealTimeUpdater.ApplicationCore.Services.Interfaces
{
	public interface IMessageService
	{
		/// <summary>
		/// This is used for getting all the messages between two users, from the database
		/// </summary>
		/// <param name="recieverId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<List<MessageResponse>> GetMessages(int recieverId, int userId);

		/// <summary>
		/// This is used for storing the messages the database
		/// </summary>
		/// <param name="messageRequest"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task SendMessage(MessageRequest messageRequest, int userId);
	}
}
