using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryServer.Models;

public class Laptop
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    public required string SKU { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }
    
    public int BrandId { get; set; }
    [ForeignKey("BrandId")]
    public Brand? Brand { get; set; }
    
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string CPU { get; set; }
    
    public int RAM_GB { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Storage { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    public int StockQuantity { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
}