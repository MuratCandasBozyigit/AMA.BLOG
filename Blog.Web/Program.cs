using Blog.Data.Shared.Abstract;
using Blog.Data.Shared.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blog.Business.Configuration;
using Blog.Core.Models;
using Blog.Core.Services;

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

// Configure Identity for AppUser and ApplicationRole
builder.Services.AddIdentity<AppUser, ApplicationRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Register RoleManager and RoleService
builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddScoped<IRoleService, RoleService>();

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
