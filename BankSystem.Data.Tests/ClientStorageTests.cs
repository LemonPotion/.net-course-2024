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
        storage.AddRange(clients);
        
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
        storage.AddRange(clients);
        
        //Act
        var youngestClient = storage.GetYoungestClient();
        var expectedYoungestClient = clients.MinBy(c => c.Age);
        
        //Assert
        youngestClient.Should().BeEquivalentTo(expectedYoungestClient);
    }
    
    [Fact]
    public void ClientStorageGetOldestClientReturnsOldestClient()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClients();
        storage.AddRange(clients);
        
        //Act
        var oldestClient = storage.GetOldestClient();
        var expectedOldestClient = clients.MaxBy(c => c.Age);
        
        //Assert
        oldestClient.Should().BeEquivalentTo(expectedOldestClient);
    }
    
    [Fact]
    public void ClientStorageGetAverageClientAgeReturnsAverageClientAge()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClients();
        storage.AddRange(clients);
        
        //Act
        var averageAge = storage.GetAverageClientAge();
        var expectedAverageAge = clients.Average(c => c.Age);
        
        //Assert
        averageAge.Should().Be(expectedAverageAge);
    }
}