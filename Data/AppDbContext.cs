using Microsoft.EntityFrameworkCore;
using Pertiv_be_with_dotnet.Models;

namespace Pertiv_be_with_dotnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().Property(u => u.Role).HasConversion<string>();

            modelBuilder.Entity<UserModel>().HasIndex(u => u.Email).IsUnique();
        }

    }
}
