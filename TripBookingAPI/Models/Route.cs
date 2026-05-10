namespace TripBookingAPI.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; } = string.Empty;
        public double Distance { get; set; }
    }
}
