using AutoMapper;
using FinancialChat.Application.Features.ChatMessages.Commands.CreateChatMessages;
using FinancialChat.Domain.UserMessages;

namespace FinancialChat.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateChatMessagesCommand, UserMessage>();
        }
    }
}
