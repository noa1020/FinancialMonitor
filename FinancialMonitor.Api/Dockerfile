FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY FinancialMonitor.Api.csproj .

RUN dotnet restore FinancialMonitor.Api.csproj

COPY . .

RUN dotnet publish FinancialMonitor.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "FinancialMonitor.Api.dll"]