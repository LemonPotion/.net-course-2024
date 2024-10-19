using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using ExportTool;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class ExportServiceTests
{
    private readonly BankSystemContext _bankSystemContext;
    private readonly ClientStorage _clientStorage;
    private readonly ExportService _exportService;
    private readonly TestDataGenerator _testDataGenerator;

    public ExportServiceTests()
    {
        _bankSystemContext = new BankSystemContext();
        _clientStorage = new ClientStorage(_bankSystemContext);
        _exportService = new ExportService(_clientStorage);
        _testDataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void ExportServiceExportClientsAddShouldExport()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients(1);
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "clients.csv");
        
        //Act
        _exportService.ExportClientsData(clients, filePath);
        
        //Assert
        File.Exists(filePath).Should().BeTrue();
    }
    
    [Fact]
    public void ExportServiceImportClientsAddShouldImport()
    {
        //Arrange
        var generatedClients = _testDataGenerator.GenerateClients(1);
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "clients.csv");
        _exportService.ExportClientsData(generatedClients, filePath);
        
        //Act
        var clients = _exportService.ImportClientsData(filePath);
        
        //Assert
        clients.Should().NotBeNull();
    }
}