using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Data;
using TripBookingAPI.Models;
using TripBookingAPI.Repositories;
using Xunit;

public class TripTests
{
    [Fact] // This tells the computer "This is a test"
    public async Task Should_Add_Trip_Successfully()
    {
        // 1. Arrange: Setup the "Fake" RAM Database
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new ApplicationDbContext(options);
        var repo = new TripRepository(context);
        var myTrip = new Trip { Destination = "Paris", Price = 500 };

        // 2. Act: Do the action
        await repo.AddAsync(myTrip);

        // 3. Assert: Verify it worked
        var count = await context.Trips.CountAsync();
        Assert.Equal(1, count);
    }
}
