using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.Commands.Chat.Models;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(SendMessageRequest command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                // Broadcast the message to everyone in the chat room
                await Clients.Group(command.ChatRoomId.ToString()).SendAsync("ReceiveMessage", response.Data);
            }
        }

        public async Task EditMessage(EditMessageRequest command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                await Clients.Group(command.ChatRoomId.ToString()).SendAsync("MessageEdited", response.Data);
            }
        }

        public async Task DeleteMessage(DeleteMessageRequest command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                await Clients.Group(command.MessageId.ToString()).SendAsync("MessageDeleted", command.MessageId);
            }
        }

        public async Task JoinRoom(int chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
            await Clients.Group(chatRoomId.ToString()).SendAsync("UserJoined", Context.User.Identity.Name);
        }

        public async Task LeaveRoom(int chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId.ToString());
            await Clients.Group(chatRoomId.ToString()).SendAsync("UserLeft", Context.User.Identity.Name);
        }
    }
}
