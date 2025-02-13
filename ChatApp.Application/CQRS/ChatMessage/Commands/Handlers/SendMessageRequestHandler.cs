using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Repositories;
using MediatR;
using System;

namespace ChatApp.Application.CQRS.Requests.Chat.Handlers
{
    public class SendMessageRequestHandler : IRequestHandler<SendMessageRequest, ApiResponse<ChatMessageResponse>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SendMessageRequestHandler(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<ChatMessageResponse>> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            var message = new ChatMessage
            {
                UserId = request.UserName,
                ChatRoomId = request.ChatRoomId,
                Message = request.Message,
                Timestamp = DateTime.UtcNow
            };

            await _chatMessageRepository.AddAsync(message);
            await _unitOfWork.CommitAsync();

            return ResponseHandler.Success(new ChatMessageResponse
            {
                MessageId = message.Id,
                Message = message.Message,
                Timestamp = message.Timestamp
            }, "Message sent successfully.");
        }
    }
}
