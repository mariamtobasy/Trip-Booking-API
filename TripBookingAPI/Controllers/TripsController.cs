using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripBookingAPI.Data;
using TripBookingAPI.DTOs;
using TripBookingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TripBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            return await _context.Trips.ToListAsync();
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        // PUT: api/Trips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, Trip trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

            _context.Entry(trip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrip", new { id = trip.Id }, trip);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }


        [HttpGet("simple")]
        public async Task<ActionResult<IEnumerable<TripDTO>>> GetSimpleTrips()
        {
            // 1. Start with the Database table
            var query = _context.Trips
                        .AsNoTracking() // Week 4 optimization!
                                        // 2. LINQ Projection: Only take what we need(TripDto)
                        .Select(t => new TripDTO
                        {
                            Destination = t.Destination,
                            Price = t.Price
                        });

            // 3. Convert the IQueryable to a List and send it back
            return await query.ToListAsync();
        }

        [HttpGet("search")]
        /*IEnumerable<TripDTO> → list of trips (but as DTOs, not full entities)
         ActionResult<> → lets you return:
          Ok(data)
          NotFound()
          BadRequest()*/
        public async Task<ActionResult<IEnumerable<TripDTO>>> SearchTrips(
       string? destination,
       decimal? minPrice,
          decimal? maxPrice,
         DateTime? departureDate,//Parameter: departureDate --> Meaning:filter by date
         string? sortBy,
          bool descending = false)//If user doesn’t send it → it's false
        {//Creating the query BUT ⚠️ not executed yet -->Equivalent to: SELECT* FROM Trips
                var query = _context.Trips
                .AsNoTracking()//EF will NOT track changes faster for read-only queries Use it when:You are NOT updating data
                .AsQueryable();//It converts the data into IQueryable so you can add more filters later on , Nothing runs until ToListAsync()
                            
            // 🔽 FILTERING STARTS HERE

            if (!string.IsNullOrEmpty(destination))//Only apply filter if user actually sent a value
            {
                query = query.Where(t => t.Destination.Contains(destination));//For each trip t, check if Destination contains the input-->WHERE Destination LIKE '%Petra%'
            }

            if (minPrice.HasValue)
            {
                query = query.Where(t => t.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(t => t.Price <= maxPrice.Value);
            }
            //in sql WHERE Price >= 50 AND Price <= 200

            if (departureDate.HasValue)
            /*👉 Why .Date?
             * Because:
             * Database date includes time
               User sends only date
              👉 Without .Date → it might not match*/


            {
                query = query.Where(t => t.DepartureDate.Date == departureDate.Value.Date);
                /*👉 You are not creating a new query
                  👉 You are adding a condition to the existing query*/
            }


            // 🔽 SORTING

            if (!string.IsNullOrEmpty(sortBy))//👉 Only sort if user sends something like:?sortBy = price
            {
                if (sortBy.ToLower() == "price")//👉 Makes input case-insensitive
                {
                    query = descending
                        ? query.OrderByDescending(t => t.Price)
                        : query.OrderBy(t => t.Price);
                    /*This is called a ternary operator

                               👉 It means:
                               if (descending)
                             use OrderByDescending
                                  else
                              use OrderBy*/
                }
                else if (sortBy.ToLower() == "date")
                {
                    query = descending
                        ? query.OrderByDescending(t => t.DepartureDate)
                        : query.OrderBy(t => t.DepartureDate);
                }
            }

            // 🔽 TEMPORARY RETURN (we’ll improve later)
            //we replaced this --> return Ok(query.ToList()); with :

            var result = query
    .Select(t => new TripDTO
    /*Select(...)

👉 Projection (Week 5)

Instead of returning full Trip, you return:

Destination
Price

👉 Faster + cleaner*/
    {
        Destination = t.Destination,
        Price = t.Price
    });

            return Ok(result.ToList());



        }


    }
    }
