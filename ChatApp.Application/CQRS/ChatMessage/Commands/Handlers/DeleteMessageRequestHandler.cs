using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Application.DTOs;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.Requests.Chat.Handlers
{
    public class DeleteMessageRequestHandler : IRequestHandler<DeleteMessageRequest, ApiResponse<DeleteMessageResponse>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMessageRequestHandler(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<DeleteMessageResponse>> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
        {
            var message = await _chatMessageRepository.GetByIdAsync(request.MessageId);

            if (message == null)
                return ResponseHandler.Failure<DeleteMessageResponse>("Message not found.");

            if (message.UserId != request.UserName)
                return ResponseHandler.Failure<DeleteMessageResponse>("You can only delete your own messages.");

            _chatMessageRepository.Delete(message);
            await _unitOfWork.CommitAsync();

            return ResponseHandler.Success(new DeleteMessageResponse
            {
                MessageId = request.MessageId,
                DeletedBy = request.UserName,
                DeletedAt = System.DateTime.UtcNow
            }, "Message deleted successfully.");
        }
    }
}
