//Note: Because we are using Identity for Users, we inherit from IdentityDbContext instead of the regular DbContext.

/*This is the most important file. It acts as the "Bridge Manager" between your C# code and the SQL Server.
 DbSet<Trip> Trips { get; set; }: This is how you tell the manager: "I want a table in my database called 'Trips', and it should follow the rules I wrote in the Trip class."*/

//base(options): This part just passes the connection information (like where the database lives) to the underlying EF Core system so it knows which server to talk to.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TripBookingAPI.Models;

//Users (already handled by Identity)

namespace TripBookingAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // These DbSets tell EF Core to create these tables in SQL
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Order> Orders { get; set; }

        //cuz there is a built in class named Route
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This is the "Seed" for the Trips table
            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 1, Destination = "Petra", Price = 50, DepartureDate = DateTime.Now.AddDays(7) },
                new Trip { Id = 2, Destination = "Wadi Rum", Price = 70, DepartureDate = DateTime.Now.AddDays(10) }
            );

            modelBuilder.Entity<Models.Route>().HasData(
              new Models.Route { Id = 1, StartLocation = "Amman", EndLocation = "Petra", Distance = 230 },
              new Models.Route { Id = 2, StartLocation = "Amman", EndLocation = "Aqaba", Distance = 330 }
);
            // You are telling the "Manager" (DbContext) how to handle the 
            // properties (navigation properties) that are already sitting in your Models.

            modelBuilder.Entity<Review>()
             .HasOne(r => r.Trip)           // Points to the property in Review.cs
             .WithMany(t => t.Reviews)      // Points to the property in Trip.cs
             .HasForeignKey(r => r.TripId); // Defines the link


            // 2. The Passenger-Ticket Rule
            modelBuilder.Entity<Ticket>()
                .HasOne(tk => tk.Passenger)       // A Ticket belongs to one Passenger
                .WithMany(p => p.Tickets)         // A Passenger has many Tickets
                .HasForeignKey(tk => tk.PassengerId); // The "Link" ID


            // 3. The Trip-Passenger Many-to-Many Rule
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Passengers)      // A Trip has many Passengers
                .WithMany(p => p.Trips);         // A Passenger has many Trips




            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    TripId = 1,
                    TotalAmount = 100
                }
            );

            // Seed Passengers
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger
                {
                    Id = 1,
                    FirstName = "Mariam"
                }
            );

            // Seed Tickets (IMPORTANT: must match OrderId & PassengerId)
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    SeatNumber = "A1",
                    Price = 50,
                    OrderId = 1,
                    PassengerId = 1
                }
            );

        }

    }
}