namespace PartnerFlowAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continue with the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "Unhandled exception occurred.");

            // Determine the HTTP status code based on exception type
            var statusCode = ex switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            // Create a detailed error response
            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = ex.Message,
                InnerException = ex.InnerException?.Message, // Include inner exception if available
                Timestamp = DateTime.Now
            };

            // Set response properties
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            // Write response
            return context.Response.WriteAsJsonAsync(errorResponse);
        }

       
    }
}
