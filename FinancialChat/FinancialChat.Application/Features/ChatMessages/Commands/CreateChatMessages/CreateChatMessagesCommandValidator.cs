using FluentValidation;

namespace FinancialChat.Application.Features.ChatMessages.Commands.CreateChatMessages
{
    public class CreateChatMessagesCommandValidator : AbstractValidator<CreateChatMessagesCommand>
    {
        public CreateChatMessagesCommandValidator()
        {
            RuleFor(p => p.Type)
               .NotEmpty().WithMessage("{Type} musn't be empty")
               .NotNull();

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} musn't be empty");
        }
    }
}
