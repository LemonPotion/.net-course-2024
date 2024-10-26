﻿using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Domain.Models;
using ExportTool;
using FluentAssertions;
using Xunit;

namespace ExportToolTests;

public class ExportServiceTests
{
    private readonly BankSystemContext _bankSystemContext;
    private readonly ExportService<Client> _exportService;
    private readonly TestDataGenerator _testDataGenerator;

    public ExportServiceTests()
    {
        _bankSystemContext = new BankSystemContext();
        _exportService = new ExportService<Client>();
        _testDataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void ExportServiceExportClientsAddShouldExport()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients(1);
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "clients.csv");

        //Act
        _exportService.ExportToCsv(clients, filePath);

        //Assert
        File.Exists(filePath).Should().BeTrue();
        var importedClients = _exportService.ImportFromCsv(filePath);
        File.Delete(filePath);
        importedClients.Should().Contain(clients);
    }

    [Fact]
    public void ExportServiceImportClientsAddShouldImport()
    {
        //Arrange
        var generatedClients = _testDataGenerator.GenerateClients(1);
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "clients.csv");
        _exportService.ExportToCsv(generatedClients, filePath);

        //Act
        var clients = _exportService.ImportFromCsv(filePath);

        //Assert
        File.Delete(filePath);
        clients.Should().NotBeNull();
        clients.Should().BeEquivalentTo(clients, options => options);
    }

    [Fact]
    public void ExportServiceSerializeToJsonShouldSerialize()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "client.json");

        //Act
        _exportService.SerializeToJson(client, filePath);

        //Assert
        File.Exists(filePath).Should().BeTrue();
        var importedClient = _exportService.DeserializeFromJson(filePath);
        File.Delete(filePath);
        importedClient.Should().NotBeNull();
        importedClient.Should().BeEquivalentTo(client);
    }

    [Fact]
    public void ExportServiceSerializeCollectionToJsonShouldSerialize()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "client_serialize.json");

        //Act
        _exportService.SerializeToJson(clients, filePath);

        //Assert
        File.Exists(filePath).Should().BeTrue();
        var importedClients = _exportService.DeserializeCollectionFromJson(filePath);
        File.Delete(filePath);

        importedClients.Should().NotBeNull();
        importedClients.Should().NotBeEmpty();
        importedClients.Should().BeEquivalentTo(clients);
    }

    [Fact]
    public void ExportServiceDeserializeFromJsonShouldDeserialize()
    {
        //Arrange
        var client = _testDataGenerator.GenerateClients(1).First();
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "client_deserialize.json");
        _exportService.SerializeToJson(client, filePath);

        //Act
        var importedClient = _exportService.DeserializeFromJson(filePath);

        //Assert
        File.Exists(filePath).Should().BeTrue();
        File.Delete(filePath);

        importedClient.Should().NotBeNull();
        importedClient.Should().BeEquivalentTo(client);
    }

    [Fact]
    public void ExportServiceDeserializeCollectionFromJsonShouldDeserialize()
    {
        //Arrange
        var clients = _testDataGenerator.GenerateClients();
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "client_serialize.json");
        _exportService.SerializeToJson(clients, filePath);

        //Act
        var importedClients = _exportService.DeserializeCollectionFromJson(filePath);

        //Assert
        File.Exists(filePath).Should().BeTrue();
        File.Delete(filePath);

        importedClients.Should().NotBeNull();
        importedClients.Should().NotBeEmpty();
        importedClients.Should().BeEquivalentTo(clients);
    }
}