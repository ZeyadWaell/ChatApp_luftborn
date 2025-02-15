﻿using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Queries.Models;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Routes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{

    [Route(ChatRoutes.Controller)]
    [ApiController]
   //Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ChatRoutes.SendMessage)]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest command)
        {
            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut($"{ChatRoutes.EditMessage}" + "/{messageId}")]
        public async Task<IActionResult> EditMessage(int messageId, [FromBody] EditMessageRequest command)
        {
            command.MessageId = messageId;
            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }


        [HttpPost(ChatRoutes.DeleteMessage)]
        public async Task<IActionResult> Delete([FromBody] DeleteMessageRequest command)
        {
            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        
        [HttpGet($"{ChatRoutes.GetMessages}" + "/{chatRoomId}")]
        public async Task<IActionResult> GetMessagesByRoom(Guid chatRoomId)
        {
            var query = new GetChatRoomMessagesQuery { ChatRoomId = chatRoomId };
            var response = await _mediator.Send(query);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }

}
