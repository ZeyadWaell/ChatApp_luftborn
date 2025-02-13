using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
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
    public class EditMessageRequestHandler : IRequestHandler<EditMessageRequest, ApiResponse<EditMessageResponse>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditMessageRequestHandler(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<EditMessageResponse>> Handle(EditMessageRequest request, CancellationToken cancellationToken)
        {
            var message = await _chatMessageRepository.GetByIdAsync(request.MessageId);

            if (message == null)
                return ResponseHandler.Failure<EditMessageResponse>("Message not found.");

            if (message.UserId != request.UserName)
                return ResponseHandler.Failure<EditMessageResponse>("You can only edit your own messages.");

            message.Message = request.NewContent;
            await _unitOfWork.CommitAsync();

            return ResponseHandler.Success(new EditMessageResponse
            {
                MessageId = message.Id,
                NewContent = message.Message,
                EditedAt = message.Timestamp
            }, "Message edited successfully.");
        }
    }
}
