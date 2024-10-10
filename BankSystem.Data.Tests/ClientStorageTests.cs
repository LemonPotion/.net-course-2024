using BankSystem.App.Services;
using BankSystem.Data.Storages;
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
        var clients = dataGenerator.GenerateClientAccounts();
        
        //Act
        storage.AddRange(clients);
        
        //Assert
        storage.Get(1, clients.Count, null).Should().BeEquivalentTo(clients.Keys);
    }

    [Fact]
    public void ClientStorageGetYoungestClientReturnsYoungestClient()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClientAccounts();
        storage.AddRange(clients);
        
        //Act
        var youngestClient = storage.GetYoungestClient();
        var expectedYoungestClient = clients.Keys.MinBy(c => c.Age);
        
        //Assert
        youngestClient.Should().BeEquivalentTo(expectedYoungestClient);
    }
    
    [Fact]
    public void ClientStorageGetOldestClientReturnsOldestClient()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClientAccounts();
        storage.AddRange(clients);
        
        //Act
        var oldestClient = storage.GetOldestClient();
        var expectedOldestClient = clients.Keys.MaxBy(c => c.Age);
        
        //Assert
        oldestClient.Should().BeEquivalentTo(expectedOldestClient);
    }
    
    [Fact]
    public void ClientStorageGetAverageClientAgeReturnsAverageClientAge()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new ClientStorage();
        var clients = dataGenerator.GenerateClientAccounts();
        storage.AddRange(clients);
        
        //Act
        var averageAge = storage.GetAverageClientAge();
        var expectedAverageAge = clients.Keys.Average(c => c.Age);
        
        //Assert
        averageAge.Should().Be(expectedAverageAge);
    }
}