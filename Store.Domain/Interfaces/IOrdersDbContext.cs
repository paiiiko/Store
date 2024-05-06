using Microsoft.EntityFrameworkCore;
using Store.Domain.Model;

namespace Store.Domain.Interfaces
{
    public interface IOrdersDbContext
    {
        DbSet<Order> Orders { get; set; }
    }
}
