using FinancialMonitor.Api.Data;
using FinancialMonitor.Api.Repositories;
using FinancialMonitor.Api.Services;
using FinancialMonitor.Api.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SignalR
builder.Services.AddSignalR();

// CORS for React
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:5173",
                    "https://localhost:5173"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// SQLite Database
builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseSqlite(
            builder.Configuration
                .GetConnectionString("DefaultConnection")
        )
);

// Dependency Injection
builder.Services.AddScoped<
    ITransactionRepository,
    TransactionRepository>();

builder.Services.AddScoped<
    ITransactionService,
    TransactionService>();

builder.Services.AddScoped<
    ITransactionBroadcaster,
    TransactionBroadcaster>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS
app.UseHttpsRedirection();

// CORS
app.UseCors("ReactApp");

// Controllers
app.MapControllers();

// SignalR Hub
app.MapHub<TransactionHub>(
    "/transactionHub"
);

app.Run();