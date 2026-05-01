# 🤖 AI-Driven Inventory Manager

A distributed system that combines the reasoning capabilities of Large Language Models (LLMs) with the reliability of a traditional backend using a custom **MCP (Model Context Protocol)** approach.  
The system allows users to query and manage an IT/Laptop inventory entirely through natural language.

---

## 🌟 Vision & Architecture

This project follows a **Hybrid Architecture**, split into two independent components:

### 1. 🧠 Orchestrator (MCP Client)
Built with Python and LangGraph.

- Acts as the "brain" of the system  
- Receives user input (natural language, e.g., Vietnamese)  
- Performs intent detection and reasoning  
- Maintains conversational context (memory)  
- Decides which tools (APIs) to call  

---

### 2. 💪 Core API (MCP Server)
Built with .NET 8 using a clean N-Tier architecture.

- Handles all business logic  
- Connects to SQL Server  
- Ensures data integrity (ACID), type safety, and security  
- **LLM never directly accesses the database**  

---

## 🚀 Tech Stack

### AI / NLP
- Python  
- LangChain  
- LangGraph  
- Ollama (Qwen 2.5 7B)

### Backend
- C# (.NET 8.0)  
- ASP.NET Core Web API  
- N-Tier Architecture  

### Database
- SQL Server  
- Entity Framework Core (Code-First)

### Communication
- JSON-RPC (inspired by Model Context Protocol - MCP)

---

## 📂 Project Structure
```text
MCP/
├── mcp_client_python/      # (Phần A) Agentic AI Orchestrator
│   ├── venv/               # Môi trường ảo Python
│   └── step4_agent.py      # Script chạy Agent giao tiếp qua Terminal
│
└── mcp_server_dotnet/      # (Phần B) Core Business API
    └── InventoryServer/
        ├── Controllers/    # Nơi nhận request (McpController, LaptopsController)
        ├── Services/       # Business Logic (InventoryService)
        ├── Models/         # Entity Models (Laptop, Brand, Category...)
        ├── Data/           # EF Core DbContext & Data Seeder
        └── Program.cs      # Cấu hình DI & Middleware Pipeline
