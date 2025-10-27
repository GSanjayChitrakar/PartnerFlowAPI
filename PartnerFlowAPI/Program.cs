using Microsoft.AspNetCore.RateLimiting;
using PartnerFlowAPI.Api;
using PartnerFlowAPI.Api.Middleware;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add controllers + views
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// Add Output Cache (must be registered before building the app)
builder.Services.AddOutputCache();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
builder.Services.AddHttpClient();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 200;
        opt.Window = TimeSpan.FromSeconds(5);
    });
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Register layered services
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration); // JWT, DB, etc.

var app = builder.Build();

// Middleware pipeline
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Authentication first
app.UseAuthentication();



// ✅ Authorization next
app.UseAuthorization();

// ✅ OutputCache before Authorization
app.UseOutputCache();

// ✅ Custom middlewares like logging should come BEFORE MapControllers
app.UseApiLogging();
app.UseResponseCompression();
app.UseRateLimiter();

//app.MapGet("/data", async (AppDbContext db) =>
//{
//    var result = await db.Data.AsNoTracking().ToListAsync();
//    return Results.Ok(result);
//})
//.CacheOutput("api");


// ✅ Finally, endpoint mapping
app.MapControllers();

// Optional MVC fallback route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
