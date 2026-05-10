using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Data;
using TripBookingAPI.DTOs;
using TripBookingAPI.Models;

// Contains business logic to fetch orders with Tickets and Passengers using EF Core

namespace TripBookingAPI.Services
{
    public class OrderService
    {
        // DbContext used to access database
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> GetOrdersWithDetails()
        {
            // Eager loading:
            // Load Orders + related Tickets + each Ticket's Passenger
            var result = _context.Orders
                .Include(o => o.Tickets)          // Load Tickets for each Order
                    .ThenInclude(t => t.Passenger) // Load Passenger for each Ticket
                .ToList();

            return result;
        }

        public async Task<bool> CreateBookingAsync(CreateBookingDTO bookingDto)
        {
            // Start the Transaction (Safety first!)
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Create the Order (Insert)
                var newOrder = new Order
                {
                    OrderDate = DateTime.Now,
                    TripId = bookingDto.TripId,
                    TotalAmount = bookingDto.Price
                };
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync(); // We save so we get the new Order.Id

                // 2. Create the Ticket (Insert)
                var newTicket = new Ticket
                {
                    SeatNumber = bookingDto.SeatNumber,
                    Price = bookingDto.Price,
                    OrderId = newOrder.Id, // Link to the order we just made
                    PassengerId = bookingDto.PassengerId
                };
                _context.Tickets.Add(newTicket);

                // 3. Save everything
                await _context.SaveChangesAsync();

                // 4. If we reached here, everything worked. COMMIT!
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // If anything failed, UNDO everything (Rollback)
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
