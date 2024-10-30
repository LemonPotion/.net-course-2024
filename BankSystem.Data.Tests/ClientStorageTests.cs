using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class ClientStorageTests
{
    private readonly BankSystemContext _bankSystemContext;
    private readonly ClientStorage _clientStorage;
    private readonly TestDataGenerator _testDataGenerator;

    public ClientStorageTests()
    {
        _bankSystemContext = new BankSystemContext();
        _clientStorage = new ClientStorage(_bankSystemContext);
        _testDataGenerator = new TestDataGenerator();
    }

    [Fact]
    public async Task ClientStorageAddClientShouldAddClientWithDefaultAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        //Act
        await _clientStorage.AddAsync(client, cancellationToken);

        //Assert
        var existingClient = await _clientStorage.GetByIdAsync(client.Id, cancellationToken); 
        
        existingClient.Should().BeEquivalentTo(client);
    }

    [Fact]
    public async Task ClientStorageGetPagedShouldReturnClients()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        //Act
        var result = await _clientStorage.GetAsync(1, _bankSystemContext.Clients.Count(), null, cancellationToken);

        //Assert
        var existingClients = await _clientStorage.GetAsync(1, _bankSystemContext.Clients.Count(), null, cancellationToken); 
        
        existingClients.Should().Contain(result);
    }

    [Fact]
    public async Task ClientStorageGetByIdShouldReturnClient()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        await _clientStorage.AddAsync(client, cancellationToken);
        var addedClient = await _bankSystemContext.Clients.FindAsync(client.Id, cancellationToken);

        //Act
        var result = await _clientStorage.GetByIdAsync(client.Id, cancellationToken);

        //Assert
        result.Should().BeEquivalentTo(addedClient);
    }

    [Fact]
    public async Task ClientStorageUpdateClientShouldUpdateClient()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var updatedClient = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        await _clientStorage.AddAsync(client, cancellationToken);
        updatedClient.Id = client.Id;

        //Act
        await _clientStorage.UpdateAsync(updatedClient.Id, updatedClient, cancellationToken);

        //Assert

        var existingClient = await _clientStorage.GetByIdAsync(client.Id, cancellationToken); 
        
        existingClient.Should().Be(updatedClient);
    }

    [Fact]
    public async Task ClientStorageDeleteClientShouldDeleteClient()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        await _clientStorage.AddAsync(client, cancellationToken);


        //Act
        await _clientStorage.DeleteAsync(client.Id, cancellationToken);

        //Assert
        var existingClient = await _clientStorage.GetByIdAsync(client.Id, cancellationToken); 
        
        existingClient.Should().BeNull();
    }

    [Fact]
    public async Task ClientStorageAddAccountShouldAddClientAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var account = _testDataGenerator.GenerateAccounts(1).First(); 
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        
        await _clientStorage.AddAsync(client, cancellationToken);
        account.ClientId = client.Id;


        //Act
        await _clientStorage.AddAccountAsync(account, cancellationToken);

        //Assert
        var existingAccount = await _clientStorage.GetAccountByIdAsync(account.Id, cancellationToken);
        existingAccount.Should().BeEquivalentTo(account);
    }

    [Fact]
    public async Task ClientStorageGetAccountByIdShouldReturnAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        await _clientStorage.AddAsync(client, cancellationToken);
        var account = client.Accounts.First();


        //Act
        var result = await _clientStorage.GetAccountByIdAsync(account.Id, cancellationToken);

        //Assert
        result.Should().BeEquivalentTo(account);
    }

    [Fact]
    public async Task ClientStorageGetAccountsPagedShouldReturnAccounts()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        await _clientStorage.AddAsync(client, cancellationToken);

        //Act
        var result = await _clientStorage.GetAccountsAsync(1, client.Accounts.Count, null, cancellationToken);

        //Assert
        var existingAccounts = await _clientStorage.GetAccountsAsync(1, client.Accounts.Count, null, cancellationToken); 
        
        existingAccounts.Should().ContainEquivalentOf(existingAccounts.First(), options => options
            .Excluding(x => x.Client)
            .Excluding(x => x.Currency)
        );
    }

    [Fact]
    public async Task ClientStorageUpdateAccountShouldUpdateAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var updatedAccount = _testDataGenerator.GenerateAccounts(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        
        await _clientStorage.AddAsync(client, cancellationToken);
        var account = client.Accounts.First();
        updatedAccount.Id = account.Id;
        updatedAccount.Client = account.Client;
        updatedAccount.Currency = account.Currency;
        updatedAccount.CurrencyId = account.CurrencyId;
        updatedAccount.ClientId = account.ClientId;

        //Act
        await _clientStorage.UpdateAccountAsync(updatedAccount.Id, updatedAccount, cancellationToken);

        //Assert
        var existingAccount = await _clientStorage.GetAccountByIdAsync(updatedAccount.Id, cancellationToken); 
        
        existingAccount.Should().BeEquivalentTo(updatedAccount, options => options
            .Excluding(a=> a.UpdatedOn));
    }

    [Fact]
    public async Task ClientStorageDeleteAccountShouldDeleteAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        
        await _clientStorage.AddAsync(client, cancellationToken);
        var account = client.Accounts.First();

        //Act
        await _clientStorage.DeleteAccountAsync(account.Id, cancellationToken);

        //Assert
        var existingAccounts = await _clientStorage.GetAccountsAsync(1, client.Accounts.Count, null, cancellationToken); 
        
        existingAccounts.Should().NotContain(account);
    }
}