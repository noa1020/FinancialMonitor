using FinancialMonitor.Api.Data;
using FinancialMonitor.Api.Entities;

namespace FinancialMonitor.Api.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _db;

    public TransactionRepository(
        AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(
        Transaction transaction,
        CancellationToken cancellationToken)
    {
        await _db.Transactions.AddAsync(
            transaction,
            cancellationToken);
    }

    public Task UpdateAsync(
        Transaction transaction,
        CancellationToken cancellationToken)
    {
        _db.Transactions.Update(transaction);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(
            cancellationToken);
    }
}