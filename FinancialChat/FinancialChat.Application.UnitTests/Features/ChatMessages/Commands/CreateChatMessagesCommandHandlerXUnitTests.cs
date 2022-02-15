using AutoMapper;
using FinancialChat.Application.Features.ChatMessages.Commands.CreateChatMessages;
using FinancialChat.Application.Mapping;
using FinancialChat.Application.UnitTests.Mocks;
using FinancialChat.Application.UnitTests.Mocks.ChatMessages;
using FinancialChat.Infrastructure.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace FinancialChat.Application.UnitTests.Features.ChatMessages.Commands
{
    public class CreateChatMessagesCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public CreateChatMessagesCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockChatMessagesRepository.AddDataChatMessagesRepository(_unitOfWork.Object.FinancialChatDbContext);
        }

        [Fact]
        public async Task CrearStreamerCommand_InputStreamer_ReturnsNumber()
        {
            var streamerInput = new CreateChatMessagesCommand
            {
                Message = "Testing",
                Type = "sent",
                UserName = "JGARCIA"
            };
            var handler = new CreateChatMessagesCommandHandler(_unitOfWork.Object, _mapper);
            var result = await handler.Handle(streamerInput, CancellationToken.None);
            result.ShouldBeOfType<int>();

        }
    }
}
