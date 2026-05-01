using Microsoft.EntityFrameworkCore;
using InventoryServer.Data;
using InventoryServer.Models;

namespace InventoryServer.Services;

public interface IInventoryService
{
    Task<List<Laptop>> GetLaptopsAsync(string? brand = null, decimal? maxPrice = null);
    Task<Laptop?> GetLaptopBySkuAsync(string sku);
}

public class InventoryService : IInventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Laptop>> GetLaptopsAsync(string? brand = null, decimal? maxPrice = null)
    {
        var query = _context.Laptops
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(l => l.Brand != null && l.Brand.Name.ToLower() == brand.ToLower());
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(l => l.Price <= maxPrice.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Laptop?> GetLaptopBySkuAsync(string sku)
    {
        return await _context.Laptops
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .FirstOrDefaultAsync(l => l.SKU == sku);
    }
}