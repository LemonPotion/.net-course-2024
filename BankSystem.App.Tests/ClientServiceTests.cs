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
    public void ClientServiceAddClientShouldAddClient()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var client = clients.First();

        //Act
        _clientService.Add(client);

        //Assert
        _bankSystemContext.Clients.Find(client.Id).Should().NotBeNull();
    }

    [Fact]
    public void ClientServiceGetPagedReturnsPagedClients()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();

        foreach (var item in clients)
        {
            _clientService.Add(item);
        }

        //Act
        var filteredClients = _clientService.GetPaged(1, clients.Count, null);

        //Assert
        filteredClients.Should().NotBeNull();
    }

    [Fact]
    public void ClientServiceUpdateClientShouldUpdateClient()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var client = clients.First();
        var updatedClient = clients.Last();

        _clientService.Add(client);
        updatedClient.Id = client.Id;

        //Act
        _clientService.Update(updatedClient.Id, updatedClient);

        //Assert
        client.Should().BeEquivalentTo(updatedClient);
    }

    [Fact]
    public void ClientServiceDeleteClientShouldDeleteClient()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var client = clients.First();

        _clientService.Add(client);

        //Act
        _clientService.Delete(client.Id);

        //Assert
        _clientStorage.Get(1, clients.Count, null).Should().NotContain(client);
    }

    [Fact]
    public void ClientServiceAddClientAccountShouldAddAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var account = _testDataGenerator.GenerateAccounts(1).First();

        _clientService.Add(client);
        account.ClientId = client.Id;

        //Act
        _clientService.AddAccount(account);
        var a = _bankSystemContext.Accounts.Find(account.Id);
        //Assert
        _bankSystemContext.Accounts.Find(account.Id).Should().BeEquivalentTo(account, options => options
            .Excluding(a => a.Currency)
            .Excluding(a => a.Client));
    }

    [Fact]
    public void ClientServiceUpdateClientAccountShouldUpdateAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var account = _testDataGenerator.GenerateAccounts(1).First();
        var updatedAccount = _testDataGenerator.GenerateAccounts(1).First();

        _clientService.Add(client);
        account.ClientId = client.Id;
        _clientService.AddAccount(account);
        updatedAccount.Id = account.Id;
        //Act
        _clientService.UpdateAccount(updatedAccount.Id, updatedAccount);

        //Assert
        _bankSystemContext.Find<Account>(account.Id).Should().BeEquivalentTo(updatedAccount, options => options
            .Excluding(a => a.Currency)
            .Excluding(a => a.Client)
            .Excluding(a => a.ClientId)
            .Excluding(a => a.CurrencyId)
            .Excluding(a => a.Id));
    }

    [Fact]
    public void ClientServiceDeleteClientAccountShouldDeleteAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients().First();
        var accounts = _testDataGenerator.GenerateAccounts();
        var account = accounts.First();

        _clientService.Add(client);
        account.ClientId = client.Id;
        _clientService.AddAccount(account);

        //Act
        _clientService.DeleteAccount(account.Id);

        //Assert
        _clientStorage.GetAccounts(1, accounts.Count, null).Should().NotContain(account);
    }
}