namespace MCRentalWeb.Middleware
{
    public class MijnMiddleware
    {
        readonly RequestDelegate _next;

        public MijnMiddleware(RequestDelegate next)
        {

            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Middleware logic goes here
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
