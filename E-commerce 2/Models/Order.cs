namespace E_commerce_2.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Stat { get; set; }
        public int Zip { get; set; }
        public string Timestamp { get; set; }

        // Navigation Proparites
        public List<OrderProduct>? orderProduct { get; set; }

    }
}
