namespace HealthCareMonitoringAPP.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // New properties
        public decimal Discount { get; set; }  // Overall discount for the order, if any

        // Calculated TotalPrice based on OrderItems and applying Discount
        public decimal TotalPrice => CalculateTotalPrice();

        // Method to calculate total price after applying the order-level discount
        public decimal CalculateTotalPrice()
        {
            // Calculate total item price including item-level discounts
            var totalItemPrice = OrderItems.Sum(item => item.Quantity * item.Price - item.Discount);

            // Apply order-level discount to the total price
            return totalItemPrice - Discount;
        }
    }
}

