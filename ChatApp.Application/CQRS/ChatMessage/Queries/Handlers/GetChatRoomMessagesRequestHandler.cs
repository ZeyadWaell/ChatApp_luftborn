using ChatApp.Application.CQRS.Queries.ChatRoom.Models;
using ChatApp.Application.DTOs;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Interfaces;
using ChatApp.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Queries.Handlers;

public class GetChatRoomMessagesRequestHandler : IRequestHandler<GetChatRoomMessagesRequest, ApiResponse<List<ChatRoomMessagesResponse>>>
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public GetChatRoomMessagesRequestHandler(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<ApiResponse<List<ChatRoomMessagesResponse>>> Handle(GetChatRoomMessagesRequest request, CancellationToken cancellationToken)
    {
        var messages = await _chatMessageRepository.GetMessagesByChatRoomAsync(request.ChatRoomId);

        if (messages == null || !messages.Any())
        {
            return ResponseHandler.Failure<List<ChatRoomMessagesResponse>>("No messages found for this chat room.");
        }

        var response = messages.Select(m => new ChatRoomMessagesResponse
        {
            MessageId = m.Id,
            Message = m.Message,
            Sender = m.User.UserName,
            Timestamp = m.Timestamp
        }).ToList();

        return ResponseHandler.Success(response, "Messages retrieved successfully.");
    }
}
