using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;
public class ApplicationDbContext : DbContext /*IdentityDbContext<AppUser>*/
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<AppUser> AppUser { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //// ASP.NET Core Identity tablolarını devre dışı bırak
        //modelBuilder.Ignore<IdentityRole>();
        //modelBuilder.Ignore<IdentityUserClaim<string>>();
        //modelBuilder.Ignore<IdentityUserLogin<string>>();
        //modelBuilder.Ignore<IdentityUserRole<string>>();
        //modelBuilder.Ignore<IdentityUserToken<string>>();
        //modelBuilder.Ignore<IdentityRoleClaim<string>>();

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
