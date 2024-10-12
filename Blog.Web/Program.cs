using Blog.Data.Shared.Abstract;
using Blog.Data.Shared.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blog.Business.Configuration;
using Blog.Core.Models;
using Blog.Core.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/account/login";
    options.LoginPath = "/account/login";
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//// Veritabanı bağlantısı için MariaDB kullanıyorsun
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(10, 5, 25))));  // MariaDB sürümünü burada belirttin

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

builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.BusinessDI();
builder.Services.RepositoryDI();

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

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
    name: "PostDetail",
    pattern: "postdetail/{category}/{tag}/{id}",
    defaults: new { controller = "PostDetail", action = "Index" }
);

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
