using FinancialMonitor.Api.DTOs;
using FinancialMonitor.Api.Entities;
using FinancialMonitor.Api.Enums;
using FinancialMonitor.Api.Repositories;
using FinancialMonitor.Api.Services;
using FluentAssertions;
using Moq;

namespace FinancialMonitor.Tests.Services;

public class TransactionServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateTransactionAndBroadcastUpdates()
    {
        // Arrange

        var repository =
            new Mock<ITransactionRepository>();

        var broadcaster =
            new Mock<ITransactionBroadcaster>();

        var service =
            new TransactionService(
                repository.Object,
                broadcaster.Object);

        var request = new CreateTransactionRequest
        {
            Amount = 100,
            Currency = Currency.USD
        };

        // Act

        var result =
            await service.CreateAsync(
                request,
                CancellationToken.None);

        // Assert

        result.Should()
            .NotBeNull();

        result.Amount
            .Should()
            .Be(100);

        result.Currency
            .Should()
            .Be(Currency.USD);

        result.TransactionId
            .Should()
            .NotBeEmpty();

        repository.Verify(
            x => x.AddAsync(
                It.IsAny<Transaction>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        repository.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Exactly(2));

        broadcaster.Verify(
            x => x.BroadcastAsync(
                It.IsAny<Transaction>()),
            Times.Exactly(2));
    }

    [Fact]
    public async Task CreateAsync_WithInvalidAmount_ShouldCreateFailedTransaction()
    {
        // Arrange

        var repository =
            new Mock<ITransactionRepository>();

        var broadcaster =
            new Mock<ITransactionBroadcaster>();

        var service =
            new TransactionService(
                repository.Object,
                broadcaster.Object);

        var request = new CreateTransactionRequest
        {
            Amount = -10,
            Currency = Currency.USD
        };

        // Act

        var result =
            await service.CreateAsync(
                request,
                CancellationToken.None);

        // Assert

        result.Status
            .Should()
            .Be(TransactionStatus.Failed);

        broadcaster.Verify(
            x => x.BroadcastAsync(
                It.IsAny<Transaction>()),
            Times.Exactly(2));
    }

    [Fact]
    public async Task CreateAsync_ShouldHandleConcurrentRequests()
    {
        // Arrange

        var repository =
            new Mock<ITransactionRepository>();

        var broadcaster =
            new Mock<ITransactionBroadcaster>();

        var service =
            new TransactionService(
                repository.Object,
                broadcaster.Object);

        // Act

        var tasks = Enumerable.Range(0, 100)
            .Select(_ =>
                service.CreateAsync(
                    new CreateTransactionRequest
                    {
                        Amount = 100,
                        Currency = Currency.USD
                    },
                    CancellationToken.None));

        var results =
            await Task.WhenAll(tasks);

        // Assert

        results.Should()
            .HaveCount(100);

        results.Select(x => x.TransactionId)
            .Distinct()
            .Should()
            .HaveCount(100);

        results.Should()
            .AllSatisfy(transaction =>
            {
                transaction.Status
                    .Should()
                    .Be(TransactionStatus.Completed);
            });

        repository.Verify(
            x => x.AddAsync(
                It.IsAny<Transaction>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(100));

        broadcaster.Verify(
            x => x.BroadcastAsync(
                It.IsAny<Transaction>()),
            Times.Exactly(200));
    }
}