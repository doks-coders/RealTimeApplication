using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Requests;
using RealTimeUpdater.Models.Response;
using Riok.Mapperly.Abstractions;

namespace RealTimeUpdater.ApplicationCore.Helpers
{

	[Mapper]
	public partial class MessageMapper
	{
		public partial Message MessageRequestToMessage(MessageRequest message);

		public partial List<MessageResponse> MessageToMessageResponse(List<Message> messages);

		public partial MessageResponse MessageRequestToRespone(MessageRequest message);
	}
}
