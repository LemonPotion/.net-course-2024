using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class RateUpdaterTests
{
    private readonly ClientStorage _clientStorage;
    private readonly TestDataGenerator _testDataGenerator;
    private readonly RateUpdater _rateUpdater;

    public RateUpdaterTests()
    {
        var bankSystemContext = new BankSystemContext();
        _clientStorage = new ClientStorage(bankSystemContext);
        _testDataGenerator = new TestDataGenerator();
        _rateUpdater = new RateUpdater(_clientStorage, TimeSpan.FromSeconds(1), 2);
    }

    [Fact]
    public async Task RateUpdateUpdateAccountsAsyncShouldUpdateAccountAmount()
    {
        // Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var account = _testDataGenerator.GenerateAccounts(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        account.UpdatedOn = DateTime.UtcNow.AddDays(-31);
        account.Amount = 100M;

        await _clientStorage.AddAsync(client, cancellationToken);
        account.ClientId = client.Id;
        
        await _clientStorage.AddAccountAsync(account, cancellationToken);

        // Act
        await _rateUpdater.UpdateAccountsAsync(cancellationToken);

        // Assert
        var updatedAccount = await _clientStorage.GetAccountByIdAsync(account.Id, cancellationToken);
        updatedAccount.Amount.Should().Be(100M * 2);
    }
}