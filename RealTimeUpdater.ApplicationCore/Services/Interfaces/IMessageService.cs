using RealTimeUpdater.Models.Requests;
using RealTimeUpdater.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUpdater.ApplicationCore.Services.Interfaces
{
	public interface IMessageService
	{
		Task<List<MessageResponse>> GetMessages(int recieverId, int userId);

		Task SendMessage(MessageRequest messageRequest, int userId);
	}
}
