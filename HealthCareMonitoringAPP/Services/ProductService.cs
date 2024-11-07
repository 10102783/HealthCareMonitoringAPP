using HealthCareMonitoringAPP.Data;
using HealthCareMonitoringAPP.Models;
using System.Collections.Generic;
using System.Linq;

namespace HealthCareMonitoringAPP.Services // Updated namespace
{
    public class ProductService
    {
        private readonly HealthCareDBContext _context;

        public ProductService(HealthCareDBContext context)
        {
            _context = context;
        }

        // Get all products
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        // Get product by ID
        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        // Create a new product
        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Update an existing product
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        // Delete a product by ID
        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
