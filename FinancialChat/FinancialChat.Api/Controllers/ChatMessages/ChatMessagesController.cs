using FinancialChat.Application.Features.ChatMessages.Commands.CreateChatMessages;
using FinancialChat.Application.Features.ChatMessages.Queries.GetMessagesList;
using FinancialChat.Domain.UserMessages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinancialChat.Api.Controllers.ChatMessages
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateMessage")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateMessage([FromBody] CreateChatMessagesCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("{username}", Name = "GetMessage")]
        [ProducesResponseType(typeof(IEnumerable<UserMessage>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserMessage>>> CreateMessage(string username)
        {
            var query = new GetMessagesListQuery(username);
            var resultList = await _mediator.Send(query);
            return Ok(resultList);
        }

    }
}
