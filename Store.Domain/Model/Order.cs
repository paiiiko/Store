namespace Store.Domain.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Product>? Products { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
    }
}