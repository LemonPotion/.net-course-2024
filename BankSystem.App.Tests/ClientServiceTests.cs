using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BankSystem.App.Tests;

public class ClientServiceTests
{
    private readonly BankSystemContext _bankSystemContext;
    private readonly ClientStorage _clientStorage;
    private readonly ClientService _clientService;
    private readonly TestDataGenerator _testDataGenerator;

    public ClientServiceTests()
    {
        _bankSystemContext = new BankSystemContext();
        _clientStorage = new ClientStorage(new BankSystemContext());
        _clientService = new ClientService(_clientStorage);
        _testDataGenerator = new TestDataGenerator();
    }

    [Fact]
    public async Task ClientServiceAddClientShouldAddClient()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var client = clients.First();
        var cancellationToken = new CancellationTokenSource().Token;

        //Act
         await _clientService.AddAsync(client, cancellationToken);

        //Assert
        var existingClient = await _bankSystemContext.Clients.FindAsync(client.Id, cancellationToken);
        
        existingClient.Should().NotBeNull();
    }
    
    [Fact]
    public async Task ClientServiceGetPagedReturnsPagedClients()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var cancellationToken = new CancellationTokenSource().Token;
        
        foreach (var item in clients)
        {
            await _clientService.AddAsync(item, cancellationToken);
        }

        //Act
        var filteredClients = _clientService.GetPagedAsync(1, clients.Count, null, cancellationToken);

        //Assert
        filteredClients.Should().NotBeNull();
    }

    [Fact]
    public async Task ClientServiceUpdateClientShouldUpdateClient()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var client = clients.First();
        var updatedClient = clients.Last();
        var cancellationToken = new CancellationTokenSource().Token;
        
        await _clientService.AddAsync(client, cancellationToken);
        updatedClient.Id = client.Id;

        //Act
        await _clientService.UpdateAsync(updatedClient.Id, updatedClient, cancellationToken);

        //Assert
        client.Should().BeEquivalentTo(updatedClient);
    }

    [Fact]
    public async Task ClientServiceDeleteClientShouldDeleteClient()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var client = clients.First();
        var cancellationToken = new CancellationTokenSource().Token;

        await _clientService.AddAsync(client, cancellationToken);

        //Act
        await _clientService.DeleteAsync(client.Id, cancellationToken);

        //Assert
        var existingClients = await _clientStorage.GetAsync(1, clients.Count, null, cancellationToken); 
        existingClients.Should().NotContain(client);
    }

    [Fact]
    public async Task ClientServiceAddClientAccountShouldAddAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var account = _testDataGenerator.GenerateAccounts(1).First();
        var cancellationToken = new CancellationTokenSource().Token;
        
        await _clientService.AddAsync(client, cancellationToken);
        account.ClientId = client.Id;

        //Act
        await _clientService.AddAccountAsync(account, cancellationToken);
        
        //Assert
        var existingClient = await _bankSystemContext.Accounts.FindAsync(account.Id);
        
        existingClient.Should().BeEquivalentTo(account, options => options
            .Excluding(a => a.Currency)
            .Excluding(a => a.Client));
    }

    [Fact]
    public async Task ClientServiceUpdateClientAccountShouldUpdateAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var account = _testDataGenerator.GenerateAccounts(1).First();
        var updatedAccount = _testDataGenerator.GenerateAccounts(1).First();
        var cancellationToken = new CancellationTokenSource().Token;
        
        await _clientService.AddAsync(client, cancellationToken);
        account.ClientId = client.Id;
        await _clientService.AddAccountAsync(account, cancellationToken);
        updatedAccount.Id = account.Id;
        //Act
        await _clientService.UpdateAccountAsync(updatedAccount.Id, updatedAccount, cancellationToken);

        //Assert
        var existingAccount = await _bankSystemContext.FindAsync<Account>(account.Id);
        
        existingAccount.Should().BeEquivalentTo(updatedAccount, options => options
            .Excluding(a => a.Currency)
            .Excluding(a => a.Client)
            .Excluding(a => a.ClientId)
            .Excluding(a => a.CurrencyId)
            .Excluding(a => a.Id));
    }

    [Fact]
    public async Task ClientServiceDeleteClientAccountShouldDeleteAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients().First();
        var accounts = _testDataGenerator.GenerateAccounts();
        var account = accounts.First();
        var cancellationToken = new CancellationTokenSource().Token;
        
        await _clientService.AddAsync(client, cancellationToken);
        account.ClientId = client.Id;
        await _clientService.AddAccountAsync(account, cancellationToken);

        //Act
        await _clientService.DeleteAccountAsync(account.Id, cancellationToken);

        //Assert
        var existingAccounts = await _clientStorage.GetAccountsAsync(1, accounts.Count, null, cancellationToken);
        
        existingAccounts.Should().NotContain(account);
    }
}