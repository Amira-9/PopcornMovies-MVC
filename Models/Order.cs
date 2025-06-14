namespace eTickets.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]  
        public ApplicationUser User { get; set; }
        public string Email { get; set; }
        //public double TotalPrice { get; set; }
        //public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
