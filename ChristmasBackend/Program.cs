using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the HTTP request pipeline.
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));



builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IReviewService, ReviewServices>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IBrandService, BrandService>();


builder.Services.AddScoped<IAdvertService, AdvertService>();



var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
