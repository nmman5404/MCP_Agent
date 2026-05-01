using Microsoft.AspNetCore.Mvc;
using InventoryServer.Services;
using InventoryServer.Models;
using System.Text.Json;

namespace InventoryServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class McpController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public McpController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    // 1. ENDPOINT KHÁM PHÁ
    [HttpGet("tools")]
    public IActionResult GetTools()
    {
        // Trả về chuẩn JSON Schema LLM đọc
        var tools = new[]
        {
            new
            {
                name = "get_laptops",
                description = "Tìm kiếm laptop trong kho dựa trên thương hiệu và mức giá tối đa.",
                parameters = new
                {
                    type = "object",
                    properties = new
                    {
                        brand = new { type = "string", description = "Tên thương hiệu (VD: Dell, Apple, Asus)" },
                        maxPrice = new { type = "number", description = "Mức giá tối đa (VD: 25000000)" }
                    }
                }
            }
        };
        return Ok(tools);
    }

    // 2. ENDPOINT THỰC THI
    [HttpPost("call")]
    public async Task<IActionResult> CallTool([FromBody] ToolCallRequest request)
    {
        // Routing: AI yêu cầu gọi Tool nào thì chạy logic Tool đó
        if (request.Name == "get_laptops")
        {
            // Bóc tách tham số JSON mà AI gửi
            string? brand = request.Arguments.ContainsKey("brand") ? request.Arguments["brand"].ToString() : null;
            
            decimal? maxPrice = null;
            if (request.Arguments.TryGetValue("maxPrice", out var priceObj) && priceObj != null)
            {
                if (decimal.TryParse(priceObj.ToString(), out decimal parsedPrice))
                {
                    maxPrice = parsedPrice;
                }
            }

            // Gọi xuống Service C#
            var result = await _inventoryService.GetLaptopsAsync(brand, maxPrice);
            
            return Ok(new { success = true, data = result });
        }

        return BadRequest(new { success = false, message = $"Tool '{request.Name}' không tồn tại trên hệ thống C#." });
    }
}