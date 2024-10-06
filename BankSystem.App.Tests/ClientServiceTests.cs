using BankSystem.App.Services;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.App.Tests;

public class ClientServiceTests
{
    [Fact]
    public void ClientServiceAddClientRangeShouldAddClients()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var clients = testDataGenerator.GenerateClients();
        
        //Act
        clientService.AddClientRange(clients);

        //Assert
        clientStorage.Clients.Should().NotBeNull();
    }
    
    [Fact]
    public void ClientServiceAddClientShouldAddClient()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var clients = testDataGenerator.GenerateClients();
        
        //Act
        clientService.AddClient(clients.First());

        //Assert
        clientStorage.Clients.Should().NotBeNull();
    }

    [Fact]
    public void ClientServiceAddClientAccountShouldAddAccount()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var client = testDataGenerator.GenerateClients().First();
        var account = testDataGenerator.GenerateAccounts().First();
        
        clientService.AddClient(client);
       
        //Act
        clientService.AddClientAccount(client, account);
        
        //Assert
        clientStorage.Clients[client].Should().Contain(account);
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
        
        clientService.AddClient(client);
        clientService.AddClientAccount(client, account);
        
        //Act
        clientService.UpdateClientAccount(client, account, updatedAccount);
        
        //Assert
        account.Should().BeEquivalentTo(updatedAccount);
    }

    [Fact]
    public void ClientServiceGetFilteredClientsReturnsFilteredClients()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientStorage = new ClientStorage();
        var clientService = new ClientService(clientStorage);
        
        var clients = testDataGenerator.GenerateClients();
        var client = clients.First();
        
        clientService.AddClientRange(clients);

        //Act
        var filteredClients = clientService.GetFilteredClients(client.FirstName, 
            client.LastName, 
            client.PhoneNumber, 
            client.PassportNumber, 
            DateTime.MinValue, 
            DateTime.MaxValue);

        //Assert
        filteredClients.Should().NotBeNull();
    }
}