namespace TripBookingAPI.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();//This is the "Many" side.
   
        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>(); // This is the "Many" side of the relationship with Trip.
    
    }
}
