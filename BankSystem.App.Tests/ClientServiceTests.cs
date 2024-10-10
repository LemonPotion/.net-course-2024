using BankSystem.App.Services;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.App.Tests;

public class ClientServiceTests
{
    [Fact]
    public void ClientServiceAddClientShouldAddClient()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var clients = testDataGenerator.GenerateClients();
        var client = clients.First();
        
        //Act
        clientService.Add(client);

        //Assert
        clientStorage.Get(1, clients.Count, null).Should().Contain(client);
    }
    
    [Fact]
    public void ClientServiceGetPagedReturnsPagedClients()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var clients = testDataGenerator.GenerateClients();

        foreach (var item in clients)
        {
            clientService.Add(item);
        }

        //Act
        var filteredClients = clientService.GetPaged(1, clients.Count, null);

        //Assert
        filteredClients.Should().NotBeNull();
    }
    
    [Fact]
    public void ClientServiceUpdateClientShouldUpdateClient()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var clients = testDataGenerator.GenerateClients();
        var client = clients.First();
        var updatedClient = clients.Last();
        updatedClient.PassportNumber = client.PassportNumber;
        
        clientService.Add(client);
        
        //Act
        clientService.Update(updatedClient);
        
        //Assert
        client.Should().BeEquivalentTo(updatedClient);
    }
    
    [Fact] 
    public void ClientServiceDeleteClientShouldDeleteClient()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var clients = testDataGenerator.GenerateClients();
        var client = clients.First();
        
        clientService.Add(client);
        
        //Act
        clientService.Delete(client);
        
        //Assert
        clientStorage.Get(1, clients.Count, null).Should().NotContain(client);
    }
    
    [Fact]
    public void ClientServiceAddClientAccountShouldAddAccount()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var client = testDataGenerator.GenerateClients().First();
        var accounts = testDataGenerator.GenerateAccounts();
        var account = accounts.First(); 
        
        clientService.Add(client);
       
        //Act
        clientService.AddAccount(client, account);
        
        //Assert
        clientStorage.GetAccounts(client,1,accounts.Count, null).Should().Contain(account);
    }
    
    [Fact]
    public void ClientServiceUpdateClientAccountShouldUpdateAccount()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var client = testDataGenerator.GenerateClients().First();
        var accounts = testDataGenerator.GenerateAccounts();
        var account = accounts.First();
        var updatedAccount = accounts.Last();
        updatedAccount.Currency = account.Currency;
        
        clientService.Add(client);
        clientService.AddAccount(client, account);
        
        //Act
        clientService.UpdateAccount(client, updatedAccount);
        
        //Assert
        account.Should().BeEquivalentTo(updatedAccount);
    }
    
    [Fact]
    public void ClientServiceDeleteClientAccountShouldDeleteAccount()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var client = testDataGenerator.GenerateClients().First();
        var accounts = testDataGenerator.GenerateAccounts();
        var account = accounts.First();
        
        clientService.Add(client);
        clientService.AddAccount(client, account);
        
        //Act
        clientService.DeleteAccount(client, account);
        
        //Assert
        clientStorage.GetAccounts(client,1,accounts.Count, null).Should().NotContain(account);
    }
}