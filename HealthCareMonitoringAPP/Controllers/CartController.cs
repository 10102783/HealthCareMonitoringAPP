using HealthCareMonitoringAPP.Data;
using HealthCareMonitoringAPP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HealthCareMonitoringAPP.Controllers
{
    public class CartController : Controller
    {
        private readonly HealthCareDBContext _context;

        public CartController(HealthCareDBContext context)
        {
            _context = context;
        }

        // GET: Cart
        public IActionResult Index()
        {
            var cart = _context.CartItems.ToList();
            return View(cart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null || quantity <= 0)
            {
                return RedirectToAction("Index", "Product");
            }

            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem == null)
            {
                // Add a new item if it doesn't exist in the cart
                cartItem = new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Quantity = quantity,
                    Price = product.Price
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                // Update the quantity if the item already exists in the cart
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Cart/PurchaseItem
        [HttpPost]
        public IActionResult PurchaseItem(int productId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                // Create the order for the purchased item
                var order = new Order
                {
                    OrderDate = System.DateTime.Now,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = cartItem.ProductId,
                            ProductName = cartItem.ProductName,
                            Quantity = cartItem.Quantity,
                            Price = cartItem.Price
                        }
                    }
                };

                // Add the order to the database
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Remove the purchased item from the cart
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            // Redirect to the Orders page or confirmation page
            return RedirectToAction("Index", "Order"); // Ensure "Index" and "Order" match your Orders page route
        }
    }
}
