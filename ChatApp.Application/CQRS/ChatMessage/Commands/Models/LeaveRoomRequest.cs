using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.Utilities.Class;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Commands.Models
{
    public class LeaveRoomRequest : IRequest<ApiResponse<ChatRoomResponse>>
    {
        public string ChatRoomId { get; set; }
        public string UserName { get; set; }
    }
}
