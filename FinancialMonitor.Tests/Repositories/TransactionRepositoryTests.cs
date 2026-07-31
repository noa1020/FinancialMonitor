using FinancialMonitor.Api.Data;
using FinancialMonitor.Api.Entities;
using FinancialMonitor.Api.Enums;
using FinancialMonitor.Api.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FinancialMonitor.Tests.Repositories;

public class TransactionRepositoryTests
{
    private AppDbContext CreateContext()
    {
        var options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString())
                .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldStoreTransaction()
    {
        await using var context =
            CreateContext();

        var repository =
            new TransactionRepository(context);

        var transaction =
            new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Amount = 100,
                Currency = Currency.USD,
                Status = TransactionStatus.Pending,
                Timestamp = DateTime.UtcNow
            };

        await repository.AddAsync(
            transaction,
            CancellationToken.None);

        await repository.SaveChangesAsync(
            CancellationToken.None);

        var result =
            await context.Transactions
                .FirstOrDefaultAsync();

        result.Should()
            .NotBeNull();

        result!.Amount
            .Should()
            .Be(100);

        result.Currency
            .Should()
            .Be(Currency.USD);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingTransaction()
    {
        await using var context =
            CreateContext();

        var transaction =
            new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Amount = 100,
                Currency = Currency.USD,
                Status = TransactionStatus.Pending,
                Timestamp = DateTime.UtcNow
            };

        context.Transactions.Add(transaction);

        await context.SaveChangesAsync();

        var repository =
            new TransactionRepository(context);

        transaction.Status =
            TransactionStatus.Completed;

        await repository.UpdateAsync(
            transaction,
            CancellationToken.None);

        await repository.SaveChangesAsync(
            CancellationToken.None);

        var result =
            await context.Transactions
                .FirstAsync();

        result.Status
            .Should()
            .Be(TransactionStatus.Completed);
    }
}