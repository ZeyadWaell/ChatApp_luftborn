 LustBorn Chat Application - CQRS & SignalR

Welcome to the **LustBorn Chat Application**, a **real-time chat system** built using **CQRS, SignalR, and Clean Architecture**. This project follows industry best practices, ensuring **scalability, maintainability, and performance**.


✅ **Real-time Chat** with **SignalR**  
✅ **CQRS Pattern** for **Clean Command-Query Separation**  
✅ **Live Room Management** (Join/Leave Rooms)  
✅ **Message CRUD Operations** (Send, Edit, Delete)  
✅ **Authentication with JWT**  
✅ **Integrated AI Bot Support** (Gemini & ChatGPT API)  
✅ **MySQL Database with Entity Framework Core**  

---

## 🎯 **Design Patterns Used**
This project follows a **scalable software architecture** using industry-standard design patterns:

| **Pattern**           | **Purpose** |
|----------------------|------------|
| **CQRS (Command Query Responsibility Segregation)** | Separates **read** and **write** operations for better scalability. |
| **Mediator Pattern** | Uses `MediatR` to decouple request handling logic. |
| **Repository Pattern** | Abstracts database operations for better maintainability. |
| **Unit of Work Pattern** | Ensures atomic transactions across repositories. |
| **Strategy Pattern** | Used for AI bot integration (`ChatGPTBotStrategy` and `GeminiBotStrategy`). |
| **Factory Pattern** | Manages the selection of bot strategies dynamically (`BotStrategyFactory`). |
| **Dependency Injection** | Ensures loose coupling of services and repositories. |
