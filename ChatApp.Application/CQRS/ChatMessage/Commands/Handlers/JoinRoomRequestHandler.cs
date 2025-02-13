using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using MediatR;

namespace ChatApp.Application.CQRS.Requests.ChatRoom.Handlers
{
    public class JoinRoomRequestHandler : IRequestHandler<JoinRoomRequest, ApiResponse<ChatRoomResponse>>
    {
        private readonly IChatService _chatService;

        public JoinRoomRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<ApiResponse<ChatRoomResponse>> Handle(JoinRoomRequest request, CancellationToken cancellationToken)
        {
            return await _chatService.JoinRoomAsync(request);
        }
    }
}
