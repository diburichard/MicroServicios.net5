using Microsoft.EntityFrameworkCore;
using MS.AFORO255.Notification.Models;

namespace MS.AFORO255.Notification.Repositories
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {
        }

        public DbSet<SendMail> SendMail { get; set; }
    }
}
