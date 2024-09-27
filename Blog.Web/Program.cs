﻿using Blog.Data.Shared.Abstract;
using Blog.Data.Shared.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blog.Business.Configuration;
using Blog.Core.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Register repository implementations
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Configure authentication
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login";
//        options.LogoutPath = "/Account/Login";
//        options.AccessDeniedPath = "/Account/Login";
//        options.Cookie.Name = "MuratBlogCookie";
//        options.SlidingExpiration = true;

//    });

builder.Services.AddIdentity<AppUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/User/Login";
    options.LoginPath = "/User/Login";
});

// DI methods for Business and Repository layers
builder.Services.BusinessDI();
builder.Services.RepositoryDI();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
