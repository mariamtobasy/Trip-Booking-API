using Microsoft.AspNetCore.Mvc;
using TripBookingAPI.Services;

namespace TripBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var result = _dashboardService.GetStatistics();
            return Ok(result);
        }
    }
}
