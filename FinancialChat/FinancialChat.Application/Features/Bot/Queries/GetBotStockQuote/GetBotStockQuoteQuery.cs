using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
