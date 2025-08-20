namespace PharmacyManagement.Models
{
    public class Medicine
    {


        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool PrescriptionRequired { get; set; }

        public ICollection<OrderMedicine> MedicinesOrder { get; set; }


    }
}
