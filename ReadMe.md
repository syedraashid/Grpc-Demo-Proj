# gRPC + REST Multi-Service Architecture

## Table of Contents
1. [Description](#description)
2. [Project Flow](#project-flow)
3. [Advantages of gRPC](#advantages-of-grpc)
4. [Project Setup](#project-setup)
5. [Directory Structure](#directory-structure)

---

## Description

This project demonstrates a **Base Level microservices architecture** where multiple services communicate using **gRPC** while also exposing **REST APIs** for external Post.

**Key Concepts:**
- **gRPC** is a high-performance, contract-based remote procedure call (RPC) framework.
- Services can **host gRPC endpoints** and **consume gRPC services** from other services.
- **Protobuf (`.proto`) files** define service contracts and messages.
- **REST APIs** are included alongside gRPC for external clients or testing purposes.
- **Shared.Protos** project centralizes `.proto` files for consistency across all services.

---

## Project Flow

1. **ServiceA (User Service)** hosts user-related data and exposes both **REST** and **gRPC** endpoints.  
   - It can **communicate with ServiceB and ServiceC** using gRPC clients.  
   - It can **create or update user data** in the **Firebase database**.

2. **ServiceB (Order Service)** consumes **ServiceA** via gRPC to fetch user details when processing or creating orders.  
   - It can also communicate with **ServiceC** using gRPC for product or analytics information. (Not Implemented yet just for example)  
   - The order data, along with user references, can be updated in **Firebase**.

3. **ServiceC (Analytics / Product Service)** consumes **ServiceA** via gRPC to get user and order insights for analytics and reporting.  
   - It can also send analytics updates or reports back to **Firebase**.  
   - It communicates with **ServiceB** if product or order correlation data is required.

4. All services share a common **Shared.Protos** project, which contains `.proto` files defining the gRPC contracts.  
   - New services can easily integrate by referencing **Shared.Protos** to automatically generate gRPC client/server code.

5. Each service exposes **REST endpoints** for testing and **gRPC endpoints** for inter-communication.

---

## Advantages of gRPC

- High-performance binary protocol (better than JSON over REST for internal service communication)
- Strongly typed contracts (compile-time safety)
- Uses HTTP/2 which is faster than HTTP/1.1
- Supports bi-directional streaming for advanced scenarios

---

## Project Setup

Use **Main branch** for running the project locally (add multiple projects as startup).  
Use the **Develop branch** for running the project containerized.

### 🧩 Run Locally

Set **multiple startup projects** (ServiceA, ServiceB, ServiceC) in Visual Studio  
or use the command line:

```bash
dotnet run --project ServiceA
dotnet run --project ServiceB
dotnet run --project ServiceC
```

### 🐳 Commands for Running in Docker

#### 🧱 Building Images (run this from the base directory of the project)
> ⚠️ First change the Firebase credentials path (let me know — I will share the file with you)

```bash
docker build -f ./ServiceA/Dockerfile -t service/servicea:dev .
docker build -f ./ServiceB/Dockerfile -t service/serviceb:dev .
docker build -f ./ServiceC/Dockerfile -t service/servicec:dev .
```

#### 🚀 Running the Application

```bash
docker-compose up -d
```

#### 🛑 Stopping the Containers

```bash
docker-compose down
```

---

## Project Ports

```
localhost:5000 - ServiceA (REST)
localhost:6001 - ServiceB (REST)
localhost:7000 - ServiceC (REST)

You can also check directly in Postman or any gRPC client using these ports:
localhost:5001 - ServiceA gRPC client for User
localhost:6002 - ServiceB gRPC client for Order
localhost:7001 - ServiceC gRPC client for Product
```

---

## Directory Structure

```
GrpcDemoSolution/
│
├── Service.Shared/ # Shared logic and proto definitions
│ ├── Protos/ # Centralized proto contracts
│ │ ├── user.proto
│ │ ├── order.proto
│ │ └── product.proto
│ ├── BaseClientServices.cs
│ ├── BaseRepository.cs
│ ├── EntityModels.cs
│ ├── ServiceExtensions.cs
│ └── Service.Shared.csproj
│
├── ServiceA/ # User Service
│ ├── Controllers/
│ ├── Grpc/
│ ├── Properties/
│ ├── Repository/
│ ├── Program.cs
│ ├── Dockerfile
│ ├── appsettings.json
│ ├── appsettings.Development.json
│ └── ServiceA.csproj
│
├── ServiceB/ # Order Service
│ ├── Controllers/
│ ├── Grpc/
│ ├── Properties/
│ ├── Repository/
│ ├── Program.cs
│ ├── Dockerfile
│ ├── appsettings.json
│ ├── appsettings.Development.json
│ └── ServiceB.csproj
│
├── ServiceC/ # Analytics / Product Service
│ ├── Controllers/
│ ├── Grpc/
│ ├── Properties/
│ ├── Repository/
│ ├── Program.cs
│ ├── Dockerfile
│ ├── appsettings.json
│ ├── appsettings.Development.json
│ └── ServiceC.csproj
│
├── firebase-credentials.json # Firebase credentials file
├── docker-compose.yml
├── GrpcDemoSolution.sln
├── .gitignore
└── ReadMe.md
```