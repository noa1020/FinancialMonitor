# Financial Monitor

Real-Time Financial Monitor MVP built with:

- Backend: .NET 8 Web API + SignalR
- Database: SQLite + Entity Framework Core
- Frontend: React + TypeScript
- Testing: xUnit + Moq + FluentAssertions


## Project Structure

```
FinancialMonitor
│
├── FinancialMonitor.Api        # .NET Backend
├── FinancialMonitor.Client     # React Frontend
├── FinancialMonitor.Tests      # Unit Tests
├── k8s                         # Kubernetes manifests
├── docs                        # Architecture decisions
└── Dockerfile
```


# Prerequisites

Install:

- .NET 8 SDK
- Node.js 20+
- Docker (optional)
- Kubernetes (optional)


Install Entity Framework CLI:

```bash
dotnet tool install --global dotnet-ef
```


# Run Locally

## Clone Repository

```bash
git clone <repository-url>

cd FinancialMonitor
```


# Backend Setup

Navigate to API project:

```bash
cd FinancialMonitor.Api
```

Restore dependencies:

```bash
dotnet restore
```

Create SQLite database:

```bash
dotnet ef database update
```

Run backend:

```bash
dotnet run
```

Backend runs on:

```
https://localhost:7213
```

Swagger:

```
https://localhost:7213/swagger
```


# Frontend Setup

Open another terminal:

```bash
cd FinancialMonitor.Client
```

Install dependencies:

```bash
npm install
```

Run:

```bash
npm run dev
```

Frontend runs on:

```
http://localhost:5173
```


## Application Routes

### Transaction Simulator

```
/add
```

Creates mock transactions and sends them to the backend API.


### Live Dashboard

```
/monitor
```

Displays real-time transaction updates using SignalR.


# Run Tests

Navigate to test project:

```bash
cd FinancialMonitor.Tests
```

Restore dependencies:

```bash
dotnet restore
```

Run tests:

```bash
dotnet test
```

Tests cover:

- Transaction processing
- Failed transactions
- Concurrent requests
- Repository persistence


# Cloud-Native & Distributed Architecture (Bonus)

## SignalR Scale-Out Problem

When deploying multiple backend replicas, each instance manages only its own connected clients.

Example:

```
Client A --> Pod A

Client B --> Pod B
```

A transaction processed by Pod A will not automatically reach clients connected to Pod B.


## Solution

Use a distributed messaging layer as a SignalR backplane.

Architecture:

```
             Transaction API
                    |
                    |
             Message Broker
          (Redis Pub/Sub / Kafka)
                    |
        +-----------+-----------+
        |                       |
      Pod A                   Pod B
    SignalR                 SignalR
        |                       |
    Clients                 Clients
```

The message broker synchronizes transaction events between backend replicas.

Possible production solutions:

- Redis Pub/Sub with SignalR Backplane
- Kafka event streaming
- Azure SignalR Service


# Docker Deployment (Bonus)

Build image:

```bash
docker build -t financial-monitor .
```

Run:

```bash
docker run -p 8080:8080 financial-monitor
```


# Kubernetes Deployment (Bonus)

Kubernetes manifests:

```
k8s/
├── deployment.yaml
└── service.yaml
```

Deploy:

```bash
kubectl apply -f k8s/
```