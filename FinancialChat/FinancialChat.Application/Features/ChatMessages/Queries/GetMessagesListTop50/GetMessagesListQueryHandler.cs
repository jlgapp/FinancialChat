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
            Func<IQueryable<UserMessage>, IOrderedQueryable<UserMessage>> orderingFunc =
                query => query.OrderBy(date => date.CreatedDate);
            
            var messagesList = await _unitOfWork.Repository<UserMessage>()
                .GetAsync(CheckForCriteria(request.UserName), 
                orderingFunc, 
                null,
                true,
                50);
            
            return (List<UserMessage>)messagesList;
        }
        public static Expression<Func<UserMessage, bool>> CheckForCriteria(string value)
        {
            return (user => user.UserName.ToUpper() == value.ToUpper());
        }
    }
}
