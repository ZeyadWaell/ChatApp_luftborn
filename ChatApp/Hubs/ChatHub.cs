using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string roomName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("UserJoined", userName, $"{userName} has joined {roomName}");
        }

        public async Task LeaveRoom(string roomName, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("UserLeft", userName, $"{userName} has left {roomName}");
        }

        public async Task SendMessage(string roomName, string userName, string message)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
        }

        public async Task EditMessage(string roomName, int messageId, string userName, string newContent)
        {
            await Clients.Group(roomName).SendAsync("MessageEdited", messageId, userName, newContent);
        }

        public async Task DeleteMessage(string roomName, int messageId, string userName)
        {
            await Clients.Group(roomName).SendAsync("MessageDeleted", messageId, userName);
        }
    }
}
