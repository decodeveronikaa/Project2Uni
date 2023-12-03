using Microsoft.AspNetCore.Authentication.Cookies;
using MVCDemoApp.Configuration;
using MVCDemoApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ConnectionStringConfig>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSession(options =>
   {
       options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as needed
   });


// Add services to the container.
builder.Services.AddControllersWithViews();
// Register DataAccessLayer as a scoped service
builder.Services.AddScoped<DataAccessLayer>();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
