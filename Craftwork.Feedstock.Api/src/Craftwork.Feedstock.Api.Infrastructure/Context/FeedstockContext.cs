using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Craftwork.Feedstock.Api.Infrastructure.Context
{
    public class FeedstockContext : DbContext
    {
        public FeedstockContext(DbContextOptions<FeedstockContext> options) : base(options)
        {
        }

        public DbSet<Color> Colors { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(ColorConfiguration.New());
        }
    }
}
