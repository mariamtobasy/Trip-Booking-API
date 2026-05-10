using Microsoft.AspNetCore.Mvc;
using TripBookingAPI.DTOs;
using TripBookingAPI.Services;
// Handles HTTP requests and exposes an endpoint to retrieve orders with related data

namespace TripBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // Service that contains the business logic
        private readonly OrderService _orderService;

        // Inject the service through constructor
        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Endpoint to get orders with related data (Tickets + Passengers)
        [HttpGet("with-details")]
        public IActionResult GetOrdersWithDetails()
        {
            // Call service method
            var result = _orderService.GetOrdersWithDetails();

            // Return data as HTTP response
            return Ok(result);
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookTrip([FromBody] CreateBookingDTO bookingDto)
        {
            var success = await _orderService.CreateBookingAsync(bookingDto);

            if (success)
            {
                return Ok(new { message = "Booking successful!" });
            }

            return BadRequest("Booking failed. Data was not saved.");
        }
    }
}
