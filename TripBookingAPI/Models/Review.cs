namespace TripBookingAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // e.g., 1 to 5
        public int TripId { get; set; } // Links to the Trip

        // FOREIGN KEY: The actual ID number of the trip in the database
        

        // NAVIGATION PROPERTY: The actual "Trip" object
        // This allows you to say "myReview.Trip.Destination"
        public virtual Trip Trip { get; set; }//This is the navigation property (for ONE):It lets you do:   review.Trip.Destination


    }
}
