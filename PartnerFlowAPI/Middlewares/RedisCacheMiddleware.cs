//using Microsoft.Extensions.Caching.Distributed;
//using System.Text;
//using System.Text.Json;

//namespace PartnerFlowAPI.Api.Middleware
//{
//    public class RedisCacheMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IDistributedCache _cache;
//        private readonly ILogger<RedisCacheMiddleware> _logger;
//        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30); // Default 30 minutes cache

//        public RedisCacheMiddleware(RequestDelegate next, IDistributedCache cache, ILogger<RedisCacheMiddleware> logger)
//        {
//            _next = next;
//            _cache = cache;
//            _logger = logger;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            // Only cache GET requests
//            if (context.Request.Method != "GET")
//            {
//                await _next(context);
//                return;
//            }

//            // Generate cache key based on endpoint, user, and parameters
//            var cacheKey = await GenerateCacheKeyAsync(context);
            
//            try
//            {
//                // Try to get cached response
//                var cachedResponse = await _cache.GetAsync(cacheKey);
                
//                if (cachedResponse != null)
//                {
//                    _logger.LogDebug("Cache hit for key: {CacheKey}", cacheKey);
                    
//                    // Return cached response
//                    var cachedData = Encoding.UTF8.GetString(cachedResponse);
//                    var responseData = JsonSerializer.Deserialize<CachedResponse>(cachedData);
                    
//                    if (responseData != null)
//                    {
//                        context.Response.StatusCode = responseData.StatusCode;
//                        context.Response.ContentType = "application/json";
                        
//                        // Add cache headers
//                        context.Response.Headers.Add("X-Cache", "HIT");
//                        context.Response.Headers.Add("X-Cache-Key", cacheKey);
                        
//                        await context.Response.WriteAsync(responseData.ResponseBody);
//                        return;
//                    }
//                }

//                _logger.LogDebug("Cache miss for key: {CacheKey}", cacheKey);
                
//                // Cache miss - capture response
//                var originalBodyStream = context.Response.Body;
//                using var responseBodyStream = new MemoryStream();
//                context.Response.Body = responseBodyStream;

//                await _next(context);

//                // Read response
//                context.Response.Body.Seek(0, SeekOrigin.Begin);
//                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
//                context.Response.Body.Seek(0, SeekOrigin.Begin);

//                // Only cache successful responses (200-299)
//                if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
//                {
//                    var responseToCache = new CachedResponse
//                    {
//                        StatusCode = context.Response.StatusCode,
//                        ResponseBody = responseBody,
//                        CachedAt = DateTime.UtcNow
//                    };

//                    var responseData = JsonSerializer.Serialize(responseToCache);
//                    var responseBytes = Encoding.UTF8.GetBytes(responseData);

//                    // Cache the response
//                    var cacheOptions = new DistributedCacheEntryOptions
//                    {
//                        AbsoluteExpirationRelativeToNow = _defaultExpiration
//                    };

//                    await _cache.SetAsync(cacheKey, responseBytes, cacheOptions);
                    
//                    // Add cache headers
//                    context.Response.Headers.Add("X-Cache", "MISS");
//                    context.Response.Headers.Add("X-Cache-Key", cacheKey);
                    
//                    _logger.LogDebug("Response cached with key: {CacheKey}", cacheKey);
//                }

//                // Return response to client
//                await responseBodyStream.CopyToAsync(originalBodyStream);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in Redis cache middleware for key: {CacheKey}", cacheKey);
//                // If caching fails, just continue with normal request processing
//                await _next(context);
//            }
//        }

//        private async Task<string> GenerateCacheKeyAsync(HttpContext context)
//        {
//            var userId = GetUserIdFromToken(context);
//            var endpoint = context.Request.Path.Value ?? "";
            
//            // Get query parameters and request body for GET requests
//            var parameters = new Dictionary<string, object>();
            
//            // Add query parameters
//            foreach (var queryParam in context.Request.Query)
//            {
//                parameters[queryParam.Key] = queryParam.Value.ToString();
//            }

//            // For GET requests, we might also have parameters in the request body (if any)
//            if (context.Request.ContentLength > 0)
//            {
//                try
//                {
//                    context.Request.EnableBuffering();
//                    context.Request.Body.Position = 0;
//                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
//                    var bodyContent = await reader.ReadToEndAsync();
//                    context.Request.Body.Position = 0;

//                    if (!string.IsNullOrEmpty(bodyContent))
//                    {
//                        var bodyParams = JsonSerializer.Deserialize<Dictionary<string, object>>(bodyContent);
//                        if (bodyParams != null)
//                        {
//                            foreach (var param in bodyParams)
//                            {
//                                parameters[param.Key] = param.Value;
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogWarning(ex, "Failed to parse request body for cache key generation");
//                }
//            }

//            // Create a deterministic cache key
//            var parametersJson = JsonSerializer.Serialize(parameters.OrderBy(x => x.Key));
//            var cacheKey = $"api_cache:{userId}:{endpoint}:{HashString(parametersJson)}";
            
//            return cacheKey;
//        }

//        private string GetUserIdFromToken(HttpContext context)
//        {
//            try
//            {
//                // Extract user ID from JWT token
//                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
//                if (authHeader != null && authHeader.StartsWith("Bearer "))
//                {
//                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    
//                    // Simple JWT token parsing to get user ID
//                    var tokenParts = token.Split('.');
//                    if (tokenParts.Length == 3)
//                    {
//                        var payload = tokenParts[1];
//                        // Add padding if needed
//                        while (payload.Length % 4 != 0)
//                            payload += "=";
                            
//                        var payloadBytes = Convert.FromBase64String(payload);
//                        var payloadJson = Encoding.UTF8.GetString(payloadBytes);
                        
//                        using var doc = JsonDocument.Parse(payloadJson);
//                        if (doc.RootElement.TryGetProperty("PartnerId", out var partnerIdElement))
//                        {
//                            return partnerIdElement.GetString() ?? "anonymous";
//                        }
//                        if (doc.RootElement.TryGetProperty("sub", out var subElement))
//                        {
//                            return subElement.GetString() ?? "anonymous";
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogWarning(ex, "Failed to extract user ID from token");
//            }

//            // Fallback to IP address if no token or token parsing fails
//            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
//            return $"ip_{clientIp}";
//        }

//        private static string HashString(string input)
//        {
//            using var sha256 = System.Security.Cryptography.SHA256.Create();
//            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
//            return Convert.ToBase64String(hashBytes)[..16]; // Take first 16 characters
//        }

//        private class CachedResponse
//        {
//            public int StatusCode { get; set; }
//            public string ResponseBody { get; set; } = string.Empty;
//            public DateTime CachedAt { get; set; }
//        }
//    }

//    // Extension method for easy registration
//    public static class RedisCacheMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseRedisCache(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<RedisCacheMiddleware>();
//        }
//    }
//}
