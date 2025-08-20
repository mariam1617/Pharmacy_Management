namespace PharmacyManagement.Models
{
    public class Order
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string StaffId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
      

        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<OrderMedicine> OrderMedicines { get; set; }

    }


}
