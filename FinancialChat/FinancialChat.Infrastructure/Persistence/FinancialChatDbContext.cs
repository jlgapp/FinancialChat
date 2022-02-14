using FinancialChat.Domain.Common;
using FinancialChat.Domain.UserMessages;
using Microsoft.EntityFrameworkCore;

namespace FinancialChat.Infrastructure.Persistence
{
    public class FinancialChatDbContext : DbContext
    {
        public FinancialChatDbContext(DbContextOptions<FinancialChatDbContext> options) : base(options)
        {
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? "SYSTEM";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedby = entry.Entity.LastModifiedby ?? "SYSTEM";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// jlga notes FLUENT API se usa cuando no se haya definido una clave foranea sin seguir las convecciones de EF

        }
        public virtual DbSet<UserMessage>? UserMessages { get; set; }
    }
}
