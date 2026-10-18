using Microsoft.EntityFrameworkCore;
using APIecommerce.Models;

namespace APIecommerce.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
}