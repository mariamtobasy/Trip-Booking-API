using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Data;
using TripBookingAPI.Interfaces;
using TripBookingAPI.Models;

namespace TripBookingAPI.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _context;

        public TripRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Get All Trips
        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await _context.Trips.ToListAsync();
        }

        // 2. Get One Trip by ID (The fix!)
        public async Task<Trip> GetByIdAsync(int id)
        {
            return await _context.Trips.FindAsync(id);
        }

        // 3. Add a New Trip
        public async Task AddAsync(Trip trip)
        {
            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
        }
    }
}
