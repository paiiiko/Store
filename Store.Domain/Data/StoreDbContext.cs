using Store.Domain.Interfaces;
using Store.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Store.Domain.Data
{
    public class StoreDbContext : DbContext, ICartDbContext,
                                             IOrdersDbContext,
                                             IProductsDbContext,
                                             IReviewDbContext,
                                             IUsersDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
