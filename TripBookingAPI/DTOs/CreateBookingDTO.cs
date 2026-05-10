namespace TripBookingAPI.DTOs
{
    public class CreateBookingDTO
    {
        public int TripId { get; set; }
        public int PassengerId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}

/*
 Read Model vs. Write Model
In professional API development, we separate objects based on what they are used for.

TripDTO (Your "Read" Model):

Goal: To show information to the user.

Fields: It only contains Destination and Price because that’s all the user needs to see.

CreateBookingDTO (Your "Write" Model):

Goal: To send information to the server to perform an action.

Fields: It needs things the user doesn't see but needs to submit, like PassengerId and SeatNumber.*/