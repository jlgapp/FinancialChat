using FinancialChat.Domain.UserMessages;
using MediatR;

namespace FinancialChat.Application.Features.ChatMessages.Queries.GetMessagesList
{
    public class GetMessagesListQuery : IRequest<List<UserMessage>>
    {
        public string UserName { get; set; } = String.Empty;

        public GetMessagesListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
