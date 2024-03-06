using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.Helpers;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Infrastructure.Repository.Repositories;
using RealTimeUpdater.Models.Requests;
using RealTimeUpdater.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUpdater.ApplicationCore.Services.Services
{
	public class MessageService:IMessageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MessageMapper _mapper;
		public MessageService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<MessageResponse>> GetMessages(int recieverId,int userId)
		{
			var messages = await _unitOfWork.Messages.GetAll(u =>
			u.RecieverId == recieverId
			&&
			u.SenderId == userId
			|| //Both Sides
			u.SenderId == recieverId
			&&
			u.RecieverId == userId
			);
			messages = messages.OrderBy(e => e.DateCreated);

			var res = _mapper.MessageToMessageResponse(messages.ToList());
			return res;

		}

		public async Task SendMessage(MessageRequest messageRequest,int userId)
		{
			var message = _mapper.MessageRequestToMessage(messageRequest);
			message.SenderId = userId;
			await _unitOfWork.Messages.Add(message);
			await _unitOfWork.Save();
		}
	}
}
