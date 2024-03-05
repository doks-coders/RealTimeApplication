using RealTimeUpdater.Models.Entities;
using RealTimeUpdater.Models.Requests;
using Riok.Mapperly.Abstractions;

namespace RealTimeUpdater.Helpers
{

	[Mapper]
	public partial class MessageMapper
	{
		public partial Message MessageRequestToMessage(MessageRequest message);
	}
}
