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

// Scoped olarak Repository'leri ekliyorsun
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// HttpContext erişimi sağlıyorsun
builder.Services.AddHttpContextAccessor();

// MVC Controller'larını kullanıyorsun
builder.Services.AddControllersWithViews();

// Cookie Authentication yapılandırması
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/account/login";
    options.LoginPath = "/account/login";
});

// Veritabanı bağlantısı için MariaDB kullanıyorsun
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 5, 25))));  // MariaDB sürümünü burada belirttin

// Identity yapılandırması
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

// Role ve servis bağımlılıklarını ekliyorsun
builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Business ve Repository DI yapılandırmaları
builder.Services.BusinessDI();
builder.Services.RepositoryDI();

// Uygulama içi telemetri (Application Insights) ekliyorsun
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Hata sayfası ve HSTS ayarları (Production'da)
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

// Routing ayarları
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
