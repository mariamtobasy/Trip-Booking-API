namespace TripBookingAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int TripId { get; set; } // This will become a Foreign Key
        public decimal TotalAmount { get; set; }

        public virtual Trip Trip { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}
