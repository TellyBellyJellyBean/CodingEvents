using CodingEvents.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Adding Persistent Database
var connectionString = builder.Configuration["coding_eventsConnectionString"];
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EventDbContext>(DbContextOptions => DbContextOptions.UseMySql(connectionString, serverVersion));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
