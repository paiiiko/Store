using Microsoft.EntityFrameworkCore;
using Store.Domain.Model;

namespace Store.Domain.Interfaces
{
    public interface IUsersDbContext
    {
        DbSet<User> Users { get; set; }

        public void Add(User profile)
        {
            Users.Add(profile);
        }
        public User? FindById(int id)
        {
            return Users.Find(id);
        }
        public Task<User?> FindByEmail(string email)
        {
            return Users.FirstOrDefaultAsync(profile => profile.Email == email);
        }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
