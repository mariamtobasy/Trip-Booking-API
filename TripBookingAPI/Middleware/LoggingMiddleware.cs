namespace TripBookingAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Logic BEFORE the request hits the Controller
            Console.WriteLine($"--- Request received: {context.Request.Method} {context.Request.Path} ---");

            // 2. Call the next middleware in the pipeline
            await _next(context);

            // 3. Logic AFTER the request is finished
            Console.WriteLine($"--- Response sent with status: {context.Response.StatusCode} ---");
        }
    }
}
