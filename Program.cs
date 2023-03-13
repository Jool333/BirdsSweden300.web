using BirdsSweden300.web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();

builder.Services.AddDbContext<BirdsContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

builder.Services.AddControllersWithViews();

var app = builder.Build();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<BirdsContext>();
    await context.Database.MigrateAsync();
    
    await SeedData.LoadBirdData(context);
}
catch (Exception ex)
{
    System.Console.WriteLine(ex.Message);
    throw;
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
