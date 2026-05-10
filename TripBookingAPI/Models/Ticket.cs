namespace TripBookingAPI.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int OrderId { get; set; } // Links to the Order
   
    
        public int PassengerId { get; set; } // Links to the Passenger
        public virtual Passenger Passenger { get; set; } = null!; // Navigation property the "One" side.


    }
}
