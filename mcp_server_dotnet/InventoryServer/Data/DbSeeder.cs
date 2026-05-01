//Hàm này tự động chèn dữ liệu test để sau này MCP Client (Python) có dữ liệu thực tế để query (Dell, Apple, Asus...).
using InventoryServer.Models;

namespace InventoryServer.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Brands.Any()) return; // Chỉ seed nếu DB trống

        var dell = new Brand { Name = "Dell", Country = "USA" };
        var apple = new Brand { Name = "Apple", Country = "USA" };
        var asus = new Brand { Name = "Asus", Country = "Taiwan" };
        context.Brands.AddRange(dell, apple, asus);
        context.SaveChanges();

        var office = new Category { Name = "Văn phòng", Description = "Mỏng nhẹ" };
        var gaming = new Category { Name = "Gaming", Description = "Hiệu năng cao" };
        context.Categories.AddRange(office, gaming);
        context.SaveChanges();

        var laptops = new List<Laptop>
        {
            new() { SKU = "DELL-INS-15", Name = "Dell Inspiron 15", BrandId = dell.Id, CategoryId = office.Id, CPU = "i5-1235U", RAM_GB = 8, Storage = "512GB SSD", Price = 15000000, StockQuantity = 10 },
            new() { SKU = "DELL-XPS-13", Name = "Dell XPS 13 Plus", BrandId = dell.Id, CategoryId = office.Id, CPU = "i7-1360P", RAM_GB = 16, Storage = "1TB SSD", Price = 35000000, StockQuantity = 2 },
            new() { SKU = "MAC-AIR-M2", Name = "MacBook Air M2", BrandId = apple.Id, CategoryId = office.Id, CPU = "M2", RAM_GB = 8, Storage = "256GB SSD", Price = 24000000, StockQuantity = 5 },
            new() { SKU = "ASUS-ROG-G15", Name = "Asus ROG Strix", BrandId = asus.Id, CategoryId = gaming.Id, CPU = "R7 6800H", RAM_GB = 16, Storage = "512GB SSD", Price = 28000000, StockQuantity = 0 }
        };
        context.Laptops.AddRange(laptops);
        context.SaveChanges();
    }
}