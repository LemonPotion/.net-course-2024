using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class ClientStorageTests
{
    [Fact]
    public void ClientStorageAddClientToStorageShouldAddSuccessfully()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClients();
        
        //Act
        foreach (var client in clients)
        {
            storage.AddClient(client);
        }
        
        //Assert
        storage.Clients.Should().BeEquivalentTo(clients);
    }

    [Fact]
    public void ClientStorageGetYoungestClientReturnsYoungestClient()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClients();
        foreach (var client in clients)
        {
            storage.AddClient(client);
        }
        
        //Act
        var youngestClient = storage.Clients.MinBy(c => c.Age);
        var expectedYoungestClient = clients.MinBy(c => c.Age);
        
        //Assert
        youngestClient.Should().BeOfType<Client>();
        youngestClient.Should().BeEquivalentTo(expectedYoungestClient);
    }
    
    [Fact]
    public void ClientStorageGetOldestClientReturnsOldestClient()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClients();
        foreach (var client in clients)
        {
            storage.AddClient(client);
        }
        
        //Act
        var oldestClient = storage.Clients.MaxBy(c => c.Age);
        var expectedOldestClient = clients.MaxBy(c => c.Age);
        
        //Assert
        oldestClient.Should().BeOfType<Client>();
        oldestClient.Should().BeEquivalentTo(expectedOldestClient);
    }
    
    [Fact]
    public void ClientStorageGetAverageClientAgeReturnsAverageClientAge()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClients();
        foreach (var client in clients)
        {
            storage.AddClient(client);
        }
        
        //Act
        var averageAge = storage.Clients.Average(c => c.Age);
        var expectedAverageAge = clients.Average(c => c.Age);
        
        //Assert
        averageAge.Should().Be(expectedAverageAge);
    }
}