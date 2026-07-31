using FinancialMonitor.Api.Entities;
using FinancialMonitor.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FinancialMonitor.Api.Services;

public class TransactionBroadcaster : ITransactionBroadcaster
{
    private readonly IHubContext<TransactionHub> _hub;

    public TransactionBroadcaster(
        IHubContext<TransactionHub> hub)
    {
        _hub = hub;
    }


    public async Task BroadcastAsync(
        Transaction transaction)
    {
        await _hub.Clients
            .All
            .SendAsync(
                "TransactionUpdated",
                transaction);
    }
}