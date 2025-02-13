using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Application.Utilities;
using ChatApp.Core.Entities;
using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChatApp.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatService(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<ChatRoomResponse>> LeaveRoomAsync(LeaveRoomRequest request)
        {
            var response = new ChatRoomResponse
            {
                ChatRoomId = request.ChatRoomId,
                RoomName = " Test :",
                Participants = new List<string> { request.UserName },
                LastActive = DateTime.UtcNow
            };
            return ResponseHandler.Success(response, $"{request.UserName} has joined {request.ChatRoomId}.");
        }



        public ChatBotInteraction ChatBotInteraction { get; set; }
        public async Task<ApiResponse<ChatMessageResponse>> SendMessageAsync(SendMessageRequest request)
        {
            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier),
                ChatRoomId = request.ChatRoomId,
                Message = request.Message,
                Timestamp = DateTime.UtcNow,
                IsBotMessage = false,
            };

            await _unitOfWork.ChatMessageRepository.AddAsync(message);
            await _unitOfWork.CompleteAsync();

            return ResponseHandler.Success(new ChatMessageResponse
            {
                MessageId = message.Id,
                Message = message.Message,
                Sender = request.UserName,
                Timestamp = message.Timestamp
            }, "Message sent successfully.");
        }

        public async Task<ApiResponse<EditMessageResponse>> EditMessageAsync(EditMessageRequest request)
        {
            var message = await _unitOfWork.ChatMessageRepository.GetByIdAsync(request.MessageId);

            if (message == null)
                return ResponseHandler.Failure<EditMessageResponse>("Message not found.");

            if (message.UserId != request.UserName)
                return ResponseHandler.Failure<EditMessageResponse>("You can only edit your own messages.");

            message.Message = request.NewContent;
            await _unitOfWork.CompleteAsync();

            return ResponseHandler.Success(new EditMessageResponse
            {
                MessageId = message.Id,
                NewContent = message.Message,
                EditedAt = message.Timestamp
            }, "Message edited successfully.");
        }

        public async Task<ApiResponse<DeleteMessageResponse>> DeleteMessageAsync(DeleteMessageRequest request)
        {
            var message = await _unitOfWork.ChatMessageRepository.GetByConditionAsync(c=>c.Id ==request.MessageId);

            if (message == null)
                return ResponseHandler.Failure<DeleteMessageResponse>("Message not found.");

            if (message.UserId != request.UserName)
                return ResponseHandler.Failure<DeleteMessageResponse>("You can only delete your own messages.");

            await _unitOfWork.ChatMessageRepository.DeleteAsync(message);
            await _unitOfWork.CompleteAsync();

            return ResponseHandler.Success(new DeleteMessageResponse
            {
                MessageId = request.MessageId,
                DeletedBy = request.UserName,
                DeletedAt = DateTime.UtcNow
            }, "Message deleted successfully.");
        }

        public async Task<ApiResponse<IList<ChatMessageResponse>>> GetChatRoomMessagesAsync(Guid chatRoomId)
        {
            var messages = await _unitOfWork.ChatMessageRepository.GetMessagesByChatRoomAsync(chatRoomId);

            if (messages == null || !messages.Any())
            {
                return ResponseHandler.Failure<IList<ChatMessageResponse>>("No messages found for this chat room.");
            }

            var response = messages.Select(m => new ChatMessageResponse
            {
                MessageId = m.Id,
                Message = m.Message,
                Sender = m.User.UserName,
                Timestamp = m.Timestamp
            }).ToList();

            return ResponseHandler.Success<IList<ChatMessageResponse>>(response, "Messages retrieved successfully.");
        }

        public Task<ApiResponse<ChatRoomResponse>> JoinRoomAsync(JoinRoomRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
