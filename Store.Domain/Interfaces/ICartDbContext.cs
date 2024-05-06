using Microsoft.EntityFrameworkCore;
using Store.Domain.Model;

namespace Store.Domain.Interfaces
{
    public interface ICartDbContext
    {
        DbSet<Cart> Carts { get; set; }
        public void Create(int id)
        {
            //Carts.
        }
    }
}
