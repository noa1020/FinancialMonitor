using FinancialMonitor.Api.Enums;

namespace FinancialMonitor.Api.DTOs;

public class CreateTransactionRequest
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}