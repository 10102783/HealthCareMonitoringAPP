namespace HealthCareMonitoringAPP.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // New properties
        public decimal Discount { get; set; }  // Discount on the cart item
        public decimal Tax { get; set; }       // Tax on the cart item

        // Computed property for total price before any discount or tax
        public decimal TotalPrice => Quantity * Price;

        // Computed property for final price after applying discount and tax
        public decimal FinalPrice => (TotalPrice - Discount) + Tax;
    }
}
