# Financial Monitor

## Prerequisites

Install:

* .NET 8 SDK
* Node.js 20+
* SQL Server
* Docker (optional)
* Kubernetes (optional)

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

## Backend Setup

Navigate to API project:

```bash
cd FinancialMonitor.Api
```

Restore packages:

```bash
dotnet restore
```

Update the SQL Server connection string in:

```text
appsettings.json
```

Create database schema:

```bash
dotnet ef database update
```

Run backend:

```bash
dotnet run
```

Backend runs on:

```text
https://localhost:7213
```

## Frontend Setup

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

```text
http://localhost:5173
```

## Run Tests

Navigate to test project:

```bash
cd FinancialMonitor.Tests
```

Restore packages:

```bash
dotnet restore
```

Run tests:

```bash
dotnet test
```

# Cloud-Native & Distributed Architecture

## Distributed Synchronization

When deploying multiple backend replicas, each pod has its own SignalR connections.

Example:

```
Client A --> Pod A

Client B --> Pod B
```

A transaction processed by Pod A will not automatically reach clients connected to Pod B.

## Solution

Use a distributed messaging layer between transaction processing and SignalR servers.

Architecture:

```
Transaction API
        |
        v
 Message Broker
(Kafka / Redis PubSub)
        |
   +----+----+
   |         |
 Pod A     Pod B
SignalR   SignalR
   |         |
Clients   Clients
```

The message broker distributes transaction events between replicas.
Each backend instance receives the event and broadcasts it to its connected clients.

Possible production solutions:

* Redis Pub/Sub with SignalR Backplane
* Kafka
* Azure SignalR Service

# Docker

Build:

```bash
docker build -t financial-monitor .
```

Run:

```bash
docker run -p 8080:8080 financial-monitor
```

# Kubernetes

Deployment manifests:

```
k8s/
 ├── deployment.yaml
 └── service.yaml
```

Deploy:

```bash
kubectl apply -f k8s/
```
