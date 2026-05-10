/*The Class (public class Trip): This represents the Table itself.
The Properties (public int Id, public string Destination): These represent the Columns (fields) in that table.
Entities: instance of a model an "Entity." If you create new Trip { Destination = "Petra" }, that specific object is an entity that will eventually become one row in your table*/


namespace TripBookingAPI.Models
{
    public class Trip
    {
        public int Id { get; set; } // EF Core automatically makes "Id" the Primary Key
        public string Destination { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }

        // NAVIGATION PROPERTY:
        // This allows you to say "myTrip.Reviews" in your code.
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        //This is the navigation property (for MANY) It lets you do:trip.Reviews

        public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}

