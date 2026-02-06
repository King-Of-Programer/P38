using Microsoft.EntityFrameworkCore;
using P38.Models;

namespace P38.Data
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.Characteristics);
            base.OnModelCreating(modelBuilder);
        }
    }
}
