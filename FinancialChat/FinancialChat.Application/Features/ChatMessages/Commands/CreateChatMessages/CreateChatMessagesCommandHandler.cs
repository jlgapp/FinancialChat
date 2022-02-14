using AutoMapper;
using FinancialChat.Application.Contracts.Persistence;
using FinancialChat.Domain.UserMessages;
using MediatR;

namespace FinancialChat.Application.Features.ChatMessages.Commands.CreateChatMessages
{
    public class CreateChatMessagesCommandHandler : IRequestHandler<CreateChatMessagesCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CreateChatMessagesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateChatMessagesCommand request, CancellationToken cancellationToken)
        {
            var chatMessageEntity =  _mapper.Map<UserMessage>(request);

            chatMessageEntity.CreatedBy = chatMessageEntity.UserName;

            _unitOfWork.Repository<UserMessage>().AddEntity(chatMessageEntity);
            var resultResponse = await _unitOfWork.Complete();

            if (resultResponse <= 0)            
                throw new Exception("Error, couldn't insert register");            

            return chatMessageEntity.Id;
        }
    }
}
