# Financial Monitor

Real-Time Financial Monitor MVP built with:

* Backend: .NET 8 Web API + SignalR
* Database: SQLite + Entity Framework Core
* Frontend: React + TypeScript
* Testing: xUnit + Moq + FluentAssertions

# Prerequisites

For running with Docker:

* Docker Desktop

For running tests locally:

* .NET 8 SDK

# Clone Repository

```bash
git clone <repository-url>

cd FinancialMonitor
```

# Run Application

Start all services:

```bash
docker compose up --build
```

The application will start:

Frontend:

```
http://localhost:3000
```

Backend API:

```
http://localhost:8080
```

Swagger:

```
http://localhost:8080/swagger
```

Stop containers:

```bash
docker compose down
```

# Application Routes

## Transaction Simulator

```
/add
```

Creates mock transactions and sends them to the backend API.

## Live Dashboard

```
/monitor
```

Displays real-time transaction updates using SignalR.

# Run Tests

Navigate to the test project:

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

* Transaction processing
* Failed transactions
* Concurrent requests
* Repository persistence

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

* Redis Pub/Sub with SignalR Backplane
* Kafka event streaming
* Azure SignalR Service

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
