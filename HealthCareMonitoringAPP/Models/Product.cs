﻿namespace HealthCareMonitoringAPP.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // New properties
        public string Category { get; set; }
        public string Dosage { get; set; }
        public string Manufacturer { get; set; }
    }
}
