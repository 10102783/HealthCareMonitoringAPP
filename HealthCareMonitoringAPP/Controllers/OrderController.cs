using HealthCareMonitoringAPP.Data; // Updated namespace
using HealthCareMonitoringAPP.Models; // Updated namespace
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthCareMonitoringAPP.Services; // For OrderService

namespace HealthCareMonitoringAPP.Controllers // Updated namespace
{
    public class OrderController : Controller
    {
        private readonly HealthCareDBContext _context;
        private readonly OrderService _orderService;

        public OrderController(HealthCareDBContext context, OrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        // POST: Order/PlaceOrder
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            // Get cart from session
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            if (cart == null || !cart.Any())
            {
                // If cart is empty, redirect to Cart page
                return RedirectToAction("Index", "Cart");
            }

            // Create the order object from cart items
            var order = new Order
            {
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                    // Removed Manufacturer, Dosage, and Category properties
                }).ToList(),
                OrderDate = DateTime.Now,
                Discount = 0 // Example: set initial discount to 0 (you can modify as needed)
            };

            // Place the order using the order service
            _orderService.PlaceOrder(order);

            // Optionally, add the order to the database
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear the cart after placing the order
            HttpContext.Session.Remove("Cart");

            // Redirect to the order confirmation page
            return RedirectToAction("OrderConfirmation");
        }

        // GET: Order/OrderConfirmation
        public IActionResult OrderConfirmation()
        {
            // Optionally, fetch order details for confirmation if needed
            return View();
        }

        // GET: Order/Index - List all orders
        public IActionResult Index()
        {
            var orders = _context.Orders.ToList(); // Fetch all orders
            return View(orders); // Return view with the list of orders
        }
    }
}

