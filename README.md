# Financial Monitor

Real-Time Financial Monitor MVP built with:

- Backend: .NET 8 Web API + SignalR
- Database: SQLite + Entity Framework Core
- Frontend: React + TypeScript
- Testing: xUnit + Moq + FluentAssertions


## Overview

Financial Monitor is a real-time transaction monitoring system.

The application allows creating simulated financial transactions and receiving live updates through SignalR.


## Prerequisites

For running the application:

- Docker Desktop

For running tests locally:

- .NET 8 SDK


## Clone Repository

```bash
git clone https://github.com/noa1020/FinancialMonitor.git

cd FinancialMonitor
```


## Run Application

Start all services using Docker Compose:

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


## Docker Services

Docker Compose starts:

- React frontend container
- .NET Web API backend container
- SQLite database volume for persistence


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

Navigate to the test project:

```bash
cd FinancialMonitor.Tests
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

The current MVP runs with a single backend instance.

When deploying multiple backend replicas, each instance manages only its own connected clients.

Example:

```
Client A --> Pod A

Client B --> Pod B
```

A transaction processed by Pod A will not automatically reach clients connected to Pod B.


## Proposed Solution

For a production-scale deployment, a distributed messaging layer can be used as a SignalR backplane.

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

The message broker synchronizes transaction events between backend replicas, allowing all SignalR instances to broadcast updates to their connected clients.


Possible production solutions:

- Redis Pub/Sub with SignalR Backplane
- Kafka event streaming
- Azure SignalR Service


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
