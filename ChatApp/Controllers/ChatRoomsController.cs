using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Routes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route(ChatRoomsRoutes.Controller)]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ChatRoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost(ChatRoomsRoutes.JoinRoom)]
        public async Task<IActionResult> JoinRoom([FromBody] JoinRoomRequest command)
        {
            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost(ChatRoomsRoutes.LeaveRoom)]
        public async Task<IActionResult> LeaveRoom([FromBody] LeaveRoomRequest command)
        {
            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
