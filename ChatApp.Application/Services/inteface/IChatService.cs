using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Application.Utilities.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Services.inteface
{
    public interface IChatService
    {
        Task<ApiResponse<ChatMessageResponse>> SendMessageAsync(SendMessageRequest request);
        Task<ApiResponse<EditMessageResponse>> EditMessageAsync(EditMessageRequest request);
        Task<ApiResponse<IList<ChatMessageResponse>>> GetChatRoomMessagesAsync(Guid chatRoomId);
        Task<ApiResponse<ChatRoomResponse>> LeaveRoomAsync(LeaveRoomRequest request);
        Task<ApiResponse<ChatRoomResponse>> JoinRoomAsync(JoinRoomRequest request);
        Task<ApiResponse<DeleteMessageResponse>> DeleteMessageAsync(DeleteMessageRequest request);

    }
}
