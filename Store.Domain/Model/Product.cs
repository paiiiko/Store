namespace Store.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? ImageData { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
        public string Category { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Order>? Orders { get; set; }
        public bool Inactive { get; set; } = false;
        public List<Cart>? Carts { get; set; }
    }
}