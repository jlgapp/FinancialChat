using FinancialChat.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Identity
{
    public class FinancialChatIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public FinancialChatIdentityDbContext(DbContextOptions<FinancialChatIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
