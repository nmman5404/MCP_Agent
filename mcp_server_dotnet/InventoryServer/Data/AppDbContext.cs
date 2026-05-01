// Đây là cầu nối chính của Entity Framework Core.
using Microsoft.EntityFrameworkCore;
using InventoryServer.Models;

namespace InventoryServer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Laptop> Laptops { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
}