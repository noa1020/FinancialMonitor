using FinancialMonitor.Api.DTOs;
using FinancialMonitor.Api.Entities;
using FinancialMonitor.Api.Repositories;

namespace FinancialMonitor.Api.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly ITransactionBroadcaster _broadcaster;

    public TransactionService(
        ITransactionRepository repository,
        ITransactionBroadcaster broadcaster)
    {
        _repository = repository;
        _broadcaster = broadcaster;
    }

    public async Task<Transaction> CreateAsync(
        CreateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            Amount = request.Amount,
            Currency = request.Currency,
            Status = TransactionStatus.Pending,
            Timestamp = DateTime.UtcNow
        };

        await _repository.AddAsync(
            transaction,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        await _broadcaster.BroadcastAsync(
            transaction);

        transaction.Status = await ProcessAsync(transaction);

        await _repository.UpdateAsync(
            transaction,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        await _broadcaster.BroadcastAsync(
            transaction);

        return transaction;
    }

    private static async Task<TransactionStatus> ProcessAsync(
        Transaction transaction)
    {
        // Business rules will be added here

        await Task.Delay(1000); // Simulate processing time
        
        if(transaction.Amount <= 0)
        {
            return TransactionStatus.Failed;
        }

        return TransactionStatus.Completed;
    }
}