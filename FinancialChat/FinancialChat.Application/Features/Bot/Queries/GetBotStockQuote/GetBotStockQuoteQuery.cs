using MediatR;

namespace FinancialChat.Application.Features.Bot.Queries.GetBotStockQuote
{
    public class GetBotStockQuoteQuery : IRequest<string>
    {
        public string Stock { get; set; } = String.Empty;       

        public GetBotStockQuoteQuery(string stock)
        {
            Stock = stock;
        }
    }
}
