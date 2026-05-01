import requests
import json
from langchain_core.tools import tool
from langchain_core.messages import HumanMessage
from langgraph.prebuilt import create_react_agent
from langchain_ollama import ChatOllama

# 1. Cấu hình C# MCP Server
MCP_SERVER_URL = "http://localhost:5176"

# 2. Khai báo Tool - Bản chất là một MCP Bridge
# Trong hệ thống Production lớn, đoạn này sẽ gọi API /mcp/tools để tự động gen (Discovery).
# Ở đây ta define trực tiếp để LangChain dễ dàng ép kiểu Pydantic.
@tool
def get_laptops(brand: str, maxPrice: float = None) -> str:
    """Tìm kiếm laptop trong kho dựa trên thương hiệu và mức giá tối đa."""
    
    print(f"\n[MCP Client] ⚡ Đang gọi sang C# Server: Brand={brand}, MaxPrice={maxPrice}")
    
    payload = {
        "name": "get_laptops",
        "arguments": {
            "brand": brand,
            "maxPrice": maxPrice
        }
    }
    
    try:
        # Bắn JSON-RPC sang cổng Call của C#
        response = requests.post(f"{MCP_SERVER_URL}/api/mcp/call", json=payload)
        
        # Trả về chuỗi JSON thô từ C# để LLM tự đọc và suy luận
        return response.text
    except Exception as e:
        return f"Lỗi kết nối đến MCP Server: {str(e)}"

# 3. Khởi tạo LLM Qwen 2.5
llm = ChatOllama(model="qwen2.5:7b", temperature=0)

# 4. Khởi tạo LangGraph Agent (Sử dụng prebuilt ReAct agent cho nhanh)
# Agent này đã tích hợp sẵn vòng lặp: Suy luận -> Gọi Tool -> Nhận kết quả -> Trả lời
tools = [get_laptops]
agent_executor = create_react_agent(llm, tools)

# 5. Hàm chạy tương tác Terminal
def run_interactive():
    print("🤖 ĐẶC VỤ QUẢN TRỊ KHO LAPTOP ĐÃ SẴN SÀNG (Gõ 'exit' để thoát)")
    print("-" * 60)
    
    while True:
        user_input = input("\nBạn: ")
        if user_input.lower() in ['exit', 'quit']:
            break
            
        print("Agent đang suy nghĩ...")
        
        # Invoke LangGraph
        result = agent_executor.invoke({"messages": [HumanMessage(content=user_input)]})
        
        # Lấy tin nhắn cuối cùng (Câu trả lời của AI)
        final_message = result["messages"][-1].content
        
        print(f"\n🤖 Qwen 2.5: {final_message}")

if __name__ == "__main__":
    run_interactive()