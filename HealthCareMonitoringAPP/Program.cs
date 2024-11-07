using HealthCareMonitoringAPP.Data;
using HealthCareMonitoringAPP.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the DbContext for HealthCareMonitoringAPP
builder.Services.AddDbContext<HealthCareDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session management for Cart (if needed)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "HealthCareMonitoringAppSession";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout as needed
    options.Cookie.IsEssential = true;
});

// Add your custom services (if you have services like ProductService, OrderService, etc.)
builder.Services.AddScoped<ProductService>();  // Assuming you have a ProductService
builder.Services.AddScoped<OrderService>();    // Assuming you have an OrderService
//builder.Services.AddScoped<CartService>();     // Assuming you have a CartService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add session and authorization middleware
app.UseSession();
app.UseAuthorization();

// Configure the default route for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
