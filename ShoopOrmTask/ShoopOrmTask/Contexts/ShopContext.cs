using Microsoft.EntityFrameworkCore;
using ShoopOrmTask.Models;

namespace ShoopOrmTask.Contexts
{
    public class ShopContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-I2J3A2U\\SQLEXPRESS;Database=Shop;Trusted_Connection=true;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
    }
}
