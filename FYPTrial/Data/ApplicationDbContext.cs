using Microsoft.EntityFrameworkCore;
using DisasterRecoverySystem.Models;

namespace DisasterRecoverySystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default admin
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Email = "admin@example.com",
                Password = "Admin@123" // Note: In production, hash the password
            });
        }
    }
}
