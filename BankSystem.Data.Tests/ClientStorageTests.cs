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
    public void ClientStorageAddClientShouldAddClientWithDefaultAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();

        //Act
        _clientStorage.Add(client);

        //Assert
        _bankSystemContext.Clients.Find(client.Id).Should().BeEquivalentTo(client);
    }

    [Fact]
    public void ClientStorageGetPagedShouldReturnClients()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients(1).First();

        //Act
        var result = _clientStorage.Get(1, _bankSystemContext.Clients.Count(), null);

        //Assert
        _bankSystemContext.Clients.Should().Contain(result);
    }

    [Fact]
    public void ClientStorageGetByIdShouldReturnClient()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        _clientStorage.Add(client);
        var addedClient = _bankSystemContext.Clients.Find(client.Id);

        //Act
        var result = _clientStorage.GetById(client.Id);

        //Assert
        result.Should().BeEquivalentTo(addedClient);
    }

    [Fact]
    public void ClientStorageUpdateClientShouldUpdateClient()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var updatedClient = _testDataGenerator.GenerateClients(1).First();
        _clientStorage.Add(client);
        updatedClient.Id = client.Id;

        //Act
        _clientStorage.Update(updatedClient);

        //Assert

        _bankSystemContext.Clients.Find(client.Id).Should().Be(updatedClient);
    }

    [Fact]
    public void ClientStorageDeleteClientShouldDeleteClient()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        _clientStorage.Add(client);


        //Act
        _clientStorage.Delete(client.Id);

        //Assert
        _bankSystemContext.Clients.Find(client.Id).Should().BeNull();
    }

    [Fact]
    public void ClientStorageAddAccountShouldAddClientAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var account = _testDataGenerator.GenerateAccounts(1).First();
        _clientStorage.Add(client);
        account.ClientId = client.Id;


        //Act
        _clientStorage.AddAccount(account);

        //Assert
        _bankSystemContext.Accounts.Find(account.Id).Should().BeEquivalentTo(account);
    }

    [Fact]
    public void ClientStorageGetAccountByIdShouldReturnAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        _clientStorage.Add(client);
        var account = client.Accounts.First();


        //Act
        var result = _clientStorage.GetAccountById(account.Id);

        //Assert
        result.Should().BeEquivalentTo(account);
    }

    [Fact]
    public void ClientStorageGetAccountsPagedShouldReturnAccounts()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        _clientStorage.Add(client);

        //Act
        var result = _clientStorage.GetAccounts(1, client.Accounts.Count, null);

        //Assert
        _bankSystemContext.Accounts.Should().Contain(client.Accounts);
    }

    [Fact]
    public void ClientStorageUpdateAccountShouldUpdateAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var updatedAccount = _testDataGenerator.GenerateAccounts(1).First();
        _clientStorage.Add(client);
        var account = client.Accounts.First();
        updatedAccount.Id = account.Id;
        updatedAccount.Client = account.Client;
        updatedAccount.Currency = account.Currency;
        updatedAccount.CurrencyId = account.CurrencyId;
        updatedAccount.ClientId = account.ClientId;

        //Act
        _clientStorage.UpdateAccount(updatedAccount);

        //Assert
        _bankSystemContext.Accounts.Find(updatedAccount.Id).Should().BeEquivalentTo(updatedAccount);
    }

    [Fact]
    public void ClientStorageDeleteAccountShouldDeleteAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        _clientStorage.Add(client);
        var account = client.Accounts.First();

        //Act
        _clientStorage.DeleteAccount(account.Id);

        //Assert
        _bankSystemContext.Accounts.Should().NotContain(account);
    }
}