using System.ComponentModel.DataAnnotations;

namespace InventoryServer.Models;

public class Brand
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(100)]
    public string? Country { get; set; }
}