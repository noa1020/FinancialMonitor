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

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp",
        policy =>
        {
            policy
                .SetIsOriginAllowed(origin =>
                {
                    return new Uri(origin).Host == "localhost";
                })
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


// Create database tables inside Docker container
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();
}


// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Disable HTTPS redirect for Docker
// app.UseHttpsRedirection();


// CORS
app.UseCors("ReactApp");


// Controllers
app.MapControllers();


// SignalR
app.MapHub<TransactionHub>(
    "/transactionHub"
);


app.Run();