using RealTimeUpdater.Models.Requests;
using RealTimeUpdater.Models.Response;

namespace RealTimeUpdater.ApplicationCore.Services.Interfaces
{
	public interface IMessageService
	{
		Task<List<MessageResponse>> GetMessages(int recieverId, int userId);

		Task SendMessage(MessageRequest messageRequest, int userId);
	}
}
