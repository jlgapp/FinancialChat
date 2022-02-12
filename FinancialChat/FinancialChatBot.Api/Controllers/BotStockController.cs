using FinancialChat.Application.Features.Bot.Queries.GetBotStockQuote;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChatBot.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BotStockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BotStockController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{stock}", Name = "GetQoute")]
        public async Task<ActionResult> GetQuote(string stock)
        {
            var query = new GetBotStockQuoteQuery(stock);
            var stockResponse = await _mediator.Send(query);
            return Ok(stockResponse);
        }
    }
}
