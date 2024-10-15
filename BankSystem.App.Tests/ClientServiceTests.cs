using BankSystem.App.Interfaces;
using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        _clientStorage.Get(1, clients.Count, null).Should().Contain(client);
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
        updatedClient.PassportNumber = client.PassportNumber;
        
        _clientService.Add(client);
        
        //Act
        _clientService.Update(updatedClient);
        
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
        var client = _testDataGenerator.GenerateClients().First();
        var accounts = _testDataGenerator.GenerateAccounts();
        var account = accounts.First();
        
        _clientService.Add(client);
       
        //Act
        _clientService.AddAccount(account);
        
        //Assert
        _clientStorage.GetAccounts(client,1,accounts.Count, null).Should().Contain(account);
    }
    
    [Fact]
    public void ClientServiceUpdateClientAccountShouldUpdateAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients().First();
        var accounts = _testDataGenerator.GenerateAccounts();
        var account = accounts.First();
        var updatedAccount = accounts.Last();
        updatedAccount.Currency = account.Currency;
        
        _clientService.Add(client);
        _clientService.AddAccount(account);
        
        //Act
        _clientService.UpdateAccount(updatedAccount);
        
        //Assert
        account.Should().BeEquivalentTo(updatedAccount);
    }
    
    [Fact]
    public void ClientServiceDeleteClientAccountShouldDeleteAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients().First();
        var accounts = _testDataGenerator.GenerateAccounts();
        var account = accounts.First();
        
        _clientService.Add(client);
        _clientService.AddAccount(account);
        
        //Act
        _clientService.DeleteAccount(account.Id);
        
        //Assert
        _clientStorage.GetAccounts(client,1,accounts.Count, null).Should().NotContain(account);
    }
}