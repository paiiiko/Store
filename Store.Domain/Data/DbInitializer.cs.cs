using Store.Domain.Data;

namespace Store.Domain.Data
{
    public class DbInitializer
    {
        public static void Initialize(StoreDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
