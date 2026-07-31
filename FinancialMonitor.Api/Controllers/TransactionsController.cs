using FinancialMonitor.Api.DTOs;
using FinancialMonitor.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialMonitor.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(
        ITransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var transaction =
            await _service.CreateAsync(
                request,
                cancellationToken);

        return Ok(transaction);
    }
}