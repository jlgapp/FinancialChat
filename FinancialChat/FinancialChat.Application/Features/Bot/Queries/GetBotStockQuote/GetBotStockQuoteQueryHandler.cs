using FinancialChat.Application.Utilites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Application.Features.Bot.Queries.GetBotStockQuote
{
    public class GetBotStockQuoteQueryHandler : IRequestHandler<GetBotStockQuoteQuery, string>
    {
        public async Task<string> Handle(GetBotStockQuoteQuery request, CancellationToken cancellationToken)
        {
            var urlBot = $"https://stooq.com/q/l/?s={request.Stock}&f=sd2t2ohlcv&h&e=csv";

            await Task.Delay(1000);

            List<string> dataStock = FilesUtilites.SplitCSV(urlBot);
            StringBuilder sb = new StringBuilder();
            foreach (var data in dataStock)
            {
                sb.Append(data);
            }
            return sb.ToString();
        }
    }
}
