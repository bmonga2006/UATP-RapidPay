using Microsoft.EntityFrameworkCore;
using RapidPay.Models;

namespace RapidPay.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Transaction> Tranactions { get; set; }
    }
}
