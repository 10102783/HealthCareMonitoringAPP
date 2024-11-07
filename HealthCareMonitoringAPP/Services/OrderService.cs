using HealthCareMonitoringAPP.Data; // Updated namespace
using HealthCareMonitoringAPP.Models; // Updated namespace
using System.Linq;

namespace HealthCareMonitoringAPP.Services // Updated namespace
{
    public class OrderService
    {
        private readonly HealthCareDBContext _context;

        public OrderService(HealthCareDBContext context)
        {
            _context = context;
        }

        // This method places the order and saves it to the database.
        public void PlaceOrder(Order order)
        {
            // Use the CalculateTotalPrice method to get the total price.
            var totalPrice = order.CalculateTotalPrice();

            // Add the order to the database
            _context.Orders.Add(order);

            // Optionally, you can also update product stock or any related entities here

            // Example: Update product stock after order is placed
            foreach (var item in order.OrderItems)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity; // Decrease stock based on the order quantity
                }
            }

            // Save changes to the database
            _context.SaveChanges();
        }
    }
}

