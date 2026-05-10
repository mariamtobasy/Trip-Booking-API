using TripBookingAPI.Models;

namespace TripBookingAPI.Interfaces
{
    public interface ITripRepository
    {
        Task<IEnumerable<Trip>> GetAllAsync();
        Task<Trip> GetByIdAsync(int id);
        Task AddAsync(Trip trip);
    }
}
