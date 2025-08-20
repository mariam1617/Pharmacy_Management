namespace PharmacyManagement.Models
{
    public class OrderMedicine
    {


        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }  
        public Order Prescription { get; set; }
        public Medicine Medicine { get; set; }

    }
}
