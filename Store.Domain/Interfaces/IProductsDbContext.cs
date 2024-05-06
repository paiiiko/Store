using Microsoft.EntityFrameworkCore;
using Store.Domain.Model;

namespace Store.Domain.Interfaces
{
    public interface IProductsDbContext
    {
        DbSet<Product> Products { get; set; }

        public List<Product> GetAll()
        {
            return Products.ToList();
        }
        public List<Product> GetByCategory(string category)
        {
            return Products.Where(product => product.Category == category)
                           .ToList();
        }
        public List<string> GetCategory()
        {
            return Products.Select(product => product.Category)
                           .Distinct()
                           .OrderBy(category => category)
                           .ToList();
        }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}