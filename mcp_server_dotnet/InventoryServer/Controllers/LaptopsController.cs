using Microsoft.AspNetCore.Mvc;
using InventoryServer.Services;

namespace InventoryServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LaptopsController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public LaptopsController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }


    // GET api/laptops?brand=BrandName&maxPrice=1000
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? brand, [FromQuery] decimal? maxPrice)
    {
        var result = await _inventoryService.GetLaptopsAsync(brand, maxPrice);
        return Ok(result);
    }

    // GET api/laptops/{sku}
    [HttpGet("{sku}")]
    public async Task<IActionResult> GetBySku(string sku)
    {
        var laptop = await _inventoryService.GetLaptopBySkuAsync(sku);
        if (laptop == null) return NotFound();
        return Ok(laptop);
    }
}