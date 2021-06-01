using Microsoft.EntityFrameworkCore;

namespace MS.AFORO255.Account.Repositories
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {
        }

        public DbSet<Models.Account> Account { get; set; }
        public DbSet<Models.Customer> Customer { get; set; }
    }
}
