using AntonPaar.Models;
using AntonPaar.Models.ReadingFiles;
using AntonPaar.ProcessData;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net.Http;
using System.Security.Permissions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IStoringContents<WordsCountModel>, StoringContent<WordsCountModel>>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<CustomConfiguration>(builder.Configuration.GetSection("CustomConfiguration"));
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
});

builder.Services.AddSession();
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
