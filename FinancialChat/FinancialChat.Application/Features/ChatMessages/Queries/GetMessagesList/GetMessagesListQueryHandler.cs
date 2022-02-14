using FinancialChat.Application.Contracts.Persistence;
using FinancialChat.Domain.UserMessages;
using MediatR;
using System.Linq.Expressions;

namespace FinancialChat.Application.Features.ChatMessages.Queries.GetMessagesList
{
    public class GetMessagesListQueryHandler : IRequestHandler<GetMessagesListQuery, List<UserMessage>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMessagesListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserMessage>> Handle(GetMessagesListQuery request, CancellationToken cancellationToken)
        {
            
            var messagesList = await _unitOfWork.Repository<UserMessage>()
                .GetAsync( CheckForCriteria("something"), null, null, true);

            throw new NotImplementedException();
        }
        public static Expression<Func<UserMessage, bool>> CheckForCriteria(string value)
        {
            return (user => user.UserName.ToUpper() == value.ToUpper());
        }
    }
}
