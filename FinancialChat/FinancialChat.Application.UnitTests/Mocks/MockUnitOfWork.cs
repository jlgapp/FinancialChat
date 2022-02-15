using FinancialChat.Infrastructure.Persistence;
using FinancialChat.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<FinancialChatDbContext>()
                .UseInMemoryDatabase(databaseName: $"FinancialChatDbContext-{dbContextId}")
                .Options
                ;

            var financialChaDbContextFake = new FinancialChatDbContext(options);

            financialChaDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(financialChaDbContextFake);

            return mockUnitOfWork;
        }

    }
}
