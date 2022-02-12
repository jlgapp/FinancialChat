using FinancialChat.Application.Features.Bot.Queries.GetBotStockQuote;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChat.Api.Controllers
{
    [Route("api/[controller]")]
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
            var videos = await _mediator.Send(query);
            return Ok(videos);
        }
    }
}
