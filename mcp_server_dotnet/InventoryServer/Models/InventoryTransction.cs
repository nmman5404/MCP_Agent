using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryServer.Models;

public class InventoryTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid LaptopId { get; set; }
    [ForeignKey("LaptopId")]
    public Laptop? Laptop { get; set; }
    
    [Required]
    [MaxLength(20)]
    public required string TransactionType { get; set; } 
    
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [MaxLength(500)]
    public string? ReferenceNote { get; set; }
}