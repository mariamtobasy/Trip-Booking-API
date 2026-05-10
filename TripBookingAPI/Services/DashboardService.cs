using TripBookingAPI.Data;

namespace TripBookingAPI.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public object GetStatistics()
        {
            // 🔹 Aggregation examples
            var totalOrders = _context.Orders.Count();
            var totalRevenue = _context.Orders.Sum(o => o.TotalAmount);
            var avgTicketPrice = _context.Tickets.Any()
                ? _context.Tickets.Average(t => t.Price)
                : 0;

            // 🔹 Grouping example
            var ordersPerTrip = _context.Orders
                .GroupBy(o => o.TripId)
                .Select(g => new
                {
                    TripId = g.Key,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.TotalAmount)
                })
                .ToList();

            return new
            {
                totalOrders,
                totalRevenue,
                avgTicketPrice,
                ordersPerTrip
            };
        }
    }
}
