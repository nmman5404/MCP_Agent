namespace InventoryServer.Models;

public class ToolCallRequest
{
    public required string Name { get; set; }
    
    // Dùng Dictionary để linh hoạt hứng bất kỳ tham số JSON nào từ AI
    public Dictionary<string, object> Arguments { get; set; } = new();
}