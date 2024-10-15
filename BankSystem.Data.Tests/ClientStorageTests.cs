using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class ClientStorageTests
{
    private readonly BankSystemContext _dbContext;
    private readonly ClientStorage _clientStorage;
    private readonly TestDataGenerator _testDataGenerator;

    public ClientStorageTests()
    {
        _dbContext = new BankSystemContext();
        _clientStorage = new ClientStorage(_dbContext);
        _testDataGenerator = new TestDataGenerator();
    }
    
    [Fact]
    public void ClientServiceAddClientShouldAddClientWithDefaultAccount()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        
        //Act
        _clientStorage.Add(client);
        var addedClient = _dbContext.Clients.Find(client.Id);
        
        //Arrange
        addedClient.Should().BeEquivalentTo(client, options => options
            .Excluding(c => c.Id));
    }
}