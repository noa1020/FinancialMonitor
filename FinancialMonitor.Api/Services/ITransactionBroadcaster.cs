using FinancialMonitor.Api.Entities;

namespace FinancialMonitor.Api.Services;

public interface ITransactionBroadcaster
{
    Task BroadcastAsync(Transaction transaction);
}
