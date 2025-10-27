
using PartnerFlowAPI.Models.Dtos;
using System.Text.Json;

namespace PartnerFlowAPI.Middlewares

{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Capture the original response stream
            var originalBodyStream = context.Response.Body;

            // Create a new memory stream to hold the response
            using (var newResponseBodyStream = new MemoryStream())
            {
                context.Response.Body = newResponseBodyStream;

                try
                {
                    // Continue processing the request
                    await _next(context);

                    // Reset the memory stream position to read the response
                    newResponseBodyStream.Seek(0, SeekOrigin.Begin);

                    // Read the response body
                    var responseBody = await new StreamReader(newResponseBodyStream).ReadToEndAsync();

                    // Reset the stream position to write it back
                    newResponseBodyStream.Seek(0, SeekOrigin.Begin);

                    // Determine if the request was successful
                    var success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;

                    // Check if the response is JSON or binary data (like an image)
                    var isJsonResponse = context.Response.ContentType != null && context.Response.ContentType.Contains("application/json");

                    object data;

                    if (isJsonResponse)
                    {
                        // Try to deserialize the response body if it's JSON
                        try
                        {
                            data = JsonSerializer.Deserialize<object>(responseBody);
                        }
                        catch
                        {
                            // If deserialization fails, treat it as plain text
                            data = responseBody;
                        }
                    }
                    else
                    {
                        // If it's binary content (e.g., image), skip further processing
                        newResponseBodyStream.Seek(0, SeekOrigin.Begin);

                        // Copy the binary content to the original stream
                        await newResponseBodyStream.CopyToAsync(originalBodyStream);
                        return;
                    }

                    // Define custom metadata
                    var metadata = new Dictionary<string, object>
     {
         { "ResponseTime", DateTime.Now.ToString("o") } // ISO 8601 format
     };

                    // Create a custom response for JSON content
                    var customResponse = new ApiResponse<object>
                    {
                        Success = success,
                        Data = data,
                        Message = success ? "Request was successful" : "Request failed",
                        StatusCode = context.Response.StatusCode,
                        Metadata = metadata
                    };

                    // Write the custom response to the original response body stream
                    context.Response.Body = originalBodyStream;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(customResponse));
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var errorResponse = new ApiResponse<object>
                    {
                        Success = false,
                        Data = null,
                        Message = ex.Message,
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Metadata = new Dictionary<string, object> { { "Error", ex.Message }, { "ResponseTime", DateTime.Now.ToString("o") } }
                    };

                    context.Response.Body = originalBodyStream;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                }
            }
        }
    }
}
