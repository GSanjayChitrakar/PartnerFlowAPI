using FGLI_SharedLibrary.Infrastructure.AzureQueue;
using FGLI_SharedLibrary.Infrastructure.Configuration;
using Microsoft.AspNetCore.RateLimiting;
using PartnerFlowAPI.Api;
using PartnerFlowAPI.Api.Middleware;
using PartnerFlowAPI.Middlewares;
using PartnerFlowAPI.Services.Configuration;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddOutputCache();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
builder.Services.AddHttpClient();
builder.Services.AddRateLimiter(options =>
{
    //options.AddFixedWindowLimiter("fixed", opt =>
    //{
    //    opt.PermitLimit = 200;
    //    opt.Window = TimeSpan.FromSeconds(5);
    //});
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, IPAddress>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress ?? IPAddress.None,
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 200,
                Window = TimeSpan.FromSeconds(5),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));
});

builder.Services.Configure<FileValidationOptions>(builder.Configuration.GetSection("FileValidation"));


builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration); // JWT, DB, etc.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PartnerFlow API V1");
        c.RoutePrefix = string.Empty; // Swagger on root
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//// Register custom exception middleware so your ExceptionMiddleware.HandleExceptionAsync runs for unhandled exceptions
//app.UseMiddleware<PartnerFlowAPI.Middlewares.ExceptionMiddleware>();

//app.UseAuthentication();
//app.UseAuthorization();
//app.UseOutputCache();
//app.UseApiLogging();
//app.UseResponseCompression();
//app.UseRateLimiter();
//app.UseMiddleware<ResponseMiddleware>();


//app.MapControllers();
app.UseHttpsRedirection();
// Move exception middleware early so it can catch most errors
app.UseMiddleware<PartnerFlowAPI.Middlewares.ExceptionMiddleware>();
// Ensure response compression runs before static files/endpoints you want compressed
app.UseResponseCompression();
//app.UseStaticFiles();
app.UseRouting();
// Apply rate limiting before auth/authorization so expensive work is avoided for rejected requests
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();
app.UseApiLogging();
// Response middleware after auth/authorization if it relies on user principal
app.UseMiddleware<ResponseMiddleware>();
app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
