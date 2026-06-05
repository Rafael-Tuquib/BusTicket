using Microsoft.EntityFrameworkCore;
using BusTicketing.Data;
using BusTicketing.Repositories.Interfaces;
using BusTicketing.Repositories.Implementations;
using BusTicketing.Services.Interfaces;
using BusTicketing.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// LocalDB connection (change if you use Docker/SQL Server differently)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=BusTicketingDb;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

// Repositories
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
builder.Services.AddScoped<IBarangayRepository, BarangayRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Services
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Ensure DB & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    var seeder = new DatabaseSeeder(db);
    await seeder.SeedAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();