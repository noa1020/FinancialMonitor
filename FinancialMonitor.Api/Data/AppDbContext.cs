using FinancialMonitor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialMonitor.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Transaction>()
            .HasKey(x => x.TransactionId);

        builder.Entity<Transaction>()
            .HasIndex(x => x.Timestamp);
    }
}