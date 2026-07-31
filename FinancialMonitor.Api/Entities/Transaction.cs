using FinancialMonitor.Api.Enums;

namespace FinancialMonitor.Api.Entities;

public class Transaction
{
    public Guid TransactionId { get; set; }

    public decimal Amount { get; set; }

    public Currency Currency { get; set; }

    public TransactionStatus Status { get; set; }

    public DateTime Timestamp { get; set; }
}