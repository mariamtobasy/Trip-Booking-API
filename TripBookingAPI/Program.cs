using Microsoft.EntityFrameworkCore;
using Serilog;
using TripBookingAPI.Data;
using TripBookingAPI.Middleware;
using TripBookingAPI.Services;

//You must assign Serilog as the global logger FIRST.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<OrderService>();


//installed Serilog activated now in this file.
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
});
//and this is for testing 
Log.Information("✅ Serilog is working correctly");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddScoped<DashboardService>();


//this is the Dependency Injection.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<LoggingMiddleware>();

/*Now, when you run the app, look at the Output Window (or the black console window). You will see a block of text that looks totally different from the hosting logs.*/

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // This simple line triggers a SQL Query!
    var count = context.Trips.Count();
    Console.WriteLine($"---> Total Trips in Database: {count}");
}


app.Run();
