using Microsoft.EntityFrameworkCore;
using pizza_server.Models;

namespace pizza_server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealSection> MealSections { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("User");
        }
    }
}
