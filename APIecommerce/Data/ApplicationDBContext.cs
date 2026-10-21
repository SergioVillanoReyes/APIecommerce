using Microsoft.EntityFrameworkCore;
using APIecommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace APIecommerce.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}