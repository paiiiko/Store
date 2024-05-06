namespace Store.Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string? Role { get; set; }
        public bool Confirmed { get; set; } = false;
        public Cart? Cart { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}