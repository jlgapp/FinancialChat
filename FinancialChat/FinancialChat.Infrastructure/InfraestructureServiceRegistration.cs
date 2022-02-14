using FinancialChat.Application.Contracts.Persistence;
using FinancialChat.Infrastructure.Persistence;
using FinancialChat.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.Infrastructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<FinancialChatDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            return services;
        }
    }
}
