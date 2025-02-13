using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.Utilities.Class;
using MediatR;

namespace ChatApp.Application.CQRS.Queries.ChatRoom.Models
{
    public class GetChatRoomMessagesRequest : IRequest<ApiResponse<List<ChatRoomMessagesResponse>>>
    {
        public string ChatRoomId { get; set; }
    }
}
