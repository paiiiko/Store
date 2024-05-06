using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Interfaces;

namespace Store.Domain.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStoreDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ICartDbContext, StoreDbContext>();
            services.AddScoped<IOrdersDbContext, StoreDbContext>();
            services.AddScoped<IProductsDbContext, StoreDbContext>();
            services.AddScoped<IReviewDbContext, StoreDbContext>();
            services.AddScoped<IUsersDbContext, StoreDbContext>();
            return services;
        }
    }
}