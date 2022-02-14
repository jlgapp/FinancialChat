using MediatR;

namespace FinancialChat.Application.Features.ChatMessages.Commands.CreateChatMessages
{
    public class CreateChatMessagesCommand : IRequest<int>
    {
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
