using FinancialChat.Application.Utilites;
using MediatR;
using System.Text;

namespace FinancialChat.Application.Features.Bot.Queries.GetBotStockQuote
{
    public class GetBotStockQuoteQueryHandler : IRequestHandler<GetBotStockQuoteQuery, string>
    {
        public async Task<string> Handle(GetBotStockQuoteQuery request, CancellationToken cancellationToken)
        {
            var urlBot = $"https://stooq.com/q/l/?s={request.Stock}&f=sd2t2ohlcv&h&e=csv";

            await Task.Delay(100);

            string dataStock = FilesUtilites.SplitCSV(urlBot);
            
            return dataStock;
        }
    }
}
