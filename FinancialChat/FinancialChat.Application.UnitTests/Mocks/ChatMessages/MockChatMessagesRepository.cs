using AutoFixture;
using FinancialChat.Domain.UserMessages;
using FinancialChat.Infrastructure.Persistence;

namespace FinancialChat.Application.UnitTests.Mocks.ChatMessages
{
    public static class MockChatMessagesRepository
    {
        public static void AddDataChatMessagesRepository(FinancialChatDbContext finacialChatDbContextFake)
        {
            var fixture = new Fixture();

            fixture.Behaviors.Add(new OmitOnRecursionBehavior()); // omitir FKs
            var srtreamers = fixture.CreateMany<UserMessage>().ToList();

            srtreamers.Add(fixture.Build<UserMessage>()
                .With(tr => tr.Id, 8001)
                //.Without(tr => tr.)
                .Create()
                );

            finacialChatDbContextFake.UserMessages!.AddRange(srtreamers);
            finacialChatDbContextFake.SaveChanges();


            //var mockRepository = new Mock<VideoRepository>(streamerDbContextFake);
            //mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(videos);
            //return mockRepository;
        }
    }
}
