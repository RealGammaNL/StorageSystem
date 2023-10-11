using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Microsoft.Extensions.Configuration;
using Domain;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<StorageDb>();
//DbContextOptions =>
//builder.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString")));

builder.Services.AddAuthentication(options => { options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; })
    .AddCookie(options => { options.LoginPath = "/account/google-login"; })
    .AddGoogle(options =>

    {
        options.ClientId = builder.Configuration["GoogleLogin:ClientId"];
        options.ClientSecret = builder.Configuration["GoogleLogin:ClientSecret"];
        options.CallbackPath = "/account/google-response";
    });

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
