using FinancialMonitor.Api.Entities;
using FinancialMonitor.Api.DTOs;

namespace FinancialMonitor.Api.Services;

public interface ITransactionService
{
    Task<Transaction> CreateAsync(
        CreateTransactionRequest request,
        CancellationToken cancellationToken);
}