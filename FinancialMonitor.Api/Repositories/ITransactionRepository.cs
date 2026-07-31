using FinancialMonitor.Api.Entities;

namespace FinancialMonitor.Api.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(
        Transaction transaction,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Transaction transaction,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}