namespace HealthCareMonitoringAPP.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }  // Foreign key for the Order
        public int ProductId { get; set; }  // Foreign key for the Product
        public string ProductName { get; set; }  // Product name for reference
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // New properties
        public decimal Discount { get; set; }  // Discount applied to this item, if any
        public decimal TotalPrice => Quantity * Price - Discount;  // Calculated total price after discount
        public string Notes { get; set; }  // Any special instructions or notes

        // Navigation properties
        public Order Order { get; set; }  // Navigation property for related Order
        public Product Product { get; set; }  // Navigation property for related Product
    }
}

