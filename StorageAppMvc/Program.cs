using Microsoft.EntityFrameworkCore;
using StorageAppMvc.Data;
using Microsoft.Extensions.Configuration;
using Domain;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<StorageDb>();
//DbContextOptions =>
//builder.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework DbContext to the container.
builder.Services.AddDbContext<StorageDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString"));
});

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
