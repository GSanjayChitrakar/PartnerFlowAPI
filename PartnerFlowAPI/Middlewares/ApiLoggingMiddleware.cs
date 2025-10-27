using Microsoft.AspNetCore.Http;
using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Database.Entities;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace PartnerFlowAPI.Api.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiLoggingMiddleware> _logger;

        public ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            var correlationId = Guid.NewGuid();
            var requestTime = DateTime.UtcNow;


            context.Response.Headers.Add("X-Correlation-ID", correlationId.ToString());

            context.Request.EnableBuffering();


            var requestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            var requestHeaders = JsonSerializer.Serialize(context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            var xForwardedFor = context.Request.Headers["X-Forwarded-For"].ToString();
            string ipAddress;
            if (!string.IsNullOrWhiteSpace(xForwardedFor))
            {

                ipAddress = xForwardedFor;
            }
            else
            {
                var xRealIp = context.Request.Headers["X-Real-IP"].ToString();
                ipAddress = !string.IsNullOrWhiteSpace(xRealIp)
                    ? xRealIp
                    : (context.Connection.RemoteIpAddress?.ToString() ?? "Unknown");
            }
            var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";


            string requestBody = string.Empty;
            if (context.Request.ContentLength > 0 && context.Request.Body.CanSeek)
            {
                context.Request.Body.Position = 0;
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }


            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            Exception? exception = null;
            var responseTime = DateTime.UtcNow;

            try
            {
                await _next(context);
                responseTime = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                exception = ex;
                responseTime = DateTime.UtcNow;
                throw;
            }
            finally
            {

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var responseHeaders = JsonSerializer.Serialize(context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
                var durationMs = (int)(responseTime - requestTime).TotalMilliseconds;


                string? errorMessage = exception?.Message;
                if (string.IsNullOrEmpty(errorMessage) && context.Response.StatusCode >= 400 && !string.IsNullOrEmpty(responseBody))
                {
                    try
                    {

                        var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);
                        if (responseJson.TryGetProperty("message", out var messageElement))
                        {
                            errorMessage = messageElement.GetString();
                        }
                        else if (responseJson.TryGetProperty("Message", out var messageElementCapital))
                        {
                            errorMessage = messageElementCapital.GetString();
                        }
                        else if (responseJson.TryGetProperty("error", out var errorElement))
                        {
                            errorMessage = errorElement.GetString();
                        }
                        else if (responseJson.TryGetProperty("Error", out var errorElementCapital))
                        {
                            errorMessage = errorElementCapital.GetString();
                        }
                    }
                    catch
                    {

                        if (context.Response.StatusCode >= 400)
                        {
                            errorMessage = responseBody.Length > 500 ? responseBody.Substring(0, 500) + "..." : responseBody;
                        }
                    }
                }


                var log = new ApiLog
                {
                    CorrelationId = correlationId,
                    RequestTime = requestTime,
                    ResponseTime = responseTime,
                    DurationMs = durationMs,
                    HttpMethod = context.Request.Method,
                    RequestUrl = requestUrl,
                    RequestHeaders = requestHeaders,
                    RequestBody = requestBody,
                    StatusCode = context.Response.StatusCode,
                    ResponseHeaders = responseHeaders,
                    ResponseBody = responseBody,
                    ExceptionMessage = errorMessage,
                    ExceptionStackTrace = exception?.StackTrace,
                    ClientIp = clientIp,
                    UserAgent = userAgent,
                    IpAddress = ipAddress,
                    CreatedAt = DateTime.UtcNow
                };

                try
                {
                    dbContext.ApiLogs.Add(log);
                    await dbContext.SaveChangesAsync();
                    _logger.LogDebug("API log saved successfully for correlation ID: {CorrelationId}", correlationId);
                }
                catch (Exception dbEx)
                {

                    _logger.LogError(dbEx, "Failed to save API log for correlation ID: {CorrelationId}", correlationId);
                }


                await responseBodyStream.CopyToAsync(originalBodyStream);
            }
        }
    }



    public static class ApiLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiLoggingMiddleware>();
        }
    }


}
