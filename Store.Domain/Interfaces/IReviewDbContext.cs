using Microsoft.EntityFrameworkCore;
using Store.Domain.Model;

namespace Store.Domain.Interfaces
{
    public interface IReviewDbContext
    {
        DbSet<Review> Reviews { get; set; }
    }
}
