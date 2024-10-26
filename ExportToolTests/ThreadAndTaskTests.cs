using System.Text;
using System.Text.Json;
using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Domain.Models;
using ExportTool;
using FluentAssertions;
using Xunit;

namespace ExportToolTests;

public class ThreadAndTaskTests
{
    private readonly BankSystemContext _bankSystemContext;
    private readonly ExportService<Client> _exportService;
    private readonly TestDataGenerator _testDataGenerator;
    private readonly object _lock = new();

    public ThreadAndTaskTests()
    {
        _bankSystemContext = new BankSystemContext();
        _exportService = new ExportService<Client>();
        _testDataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void ExportServiceThreadSerializeToJsonShouldSerializeToFiles()
    {
        // Arrange
        var clientsA = _testDataGenerator.GenerateClients();
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var fileName = "clients.json";
        var filePath = Path.Combine(desktopPath, fileName);

        var maxFileSize = 60 * 1024;

        // Создание потоков
        var threadA = new Thread(() => SerializeClients(clientsA, filePath, maxFileSize));
        var threadB = new Thread(() => SerializeClients(clientsA, filePath, maxFileSize));

        // Act
        threadA.Start();
        threadB.Start();

        threadA.Join();
        threadB.Join();

        // Assert
        var importedClients = _exportService.DeserializeCollectionFromJson(filePath);
        importedClients.Should().NotBeNull();
        importedClients.Should().NotBeEmpty();

        var jsonFiles = Directory.GetFiles(desktopPath, "*.json");
        jsonFiles.Should().NotBeNull();
        jsonFiles.Should().NotBeEmpty();

        foreach (var file in jsonFiles)
        {
            File.Delete(file);
        }
    }

    private void SerializeClients(IEnumerable<Client> clients, string filePath, int maxFileSize)
    {
        var currentFileSize = 0;
        var currentFilePath = filePath;
        var counter = 0;
        var clientsList = new List<Client>();

        foreach (var client in clients)
        {
            var jsonData = JsonSerializer.Serialize(client);
            var dataSize = Encoding.UTF8.GetByteCount(jsonData);
            currentFileSize += dataSize;

            clientsList.Add(client);

            if (currentFileSize <= maxFileSize) continue;

            lock (_lock)
            {
                _exportService.SerializeToJson(clientsList, currentFilePath);
            }

            counter++;
            currentFilePath = Path.Combine(Path.GetDirectoryName(filePath),
                $"{Path.GetFileNameWithoutExtension(filePath)} ({counter}).json");
            currentFileSize = 0;
            clientsList.Clear();
        }

        if (clientsList.Count > 0)
        {
            _exportService.SerializeToJson(clientsList, currentFilePath);
        }
    }

    [Fact]
    public void Test2()
    {
        var account = _testDataGenerator.GenerateAccounts().First();
        var initialAmount = account.Amount;

        var threadA = new Thread(() =>
        {
            for (var i = 0; i < 10; i++)
            {
                account.Amount += 100;
            }
        });

        var threadB = new Thread(() =>
        {
            for (var i = 0; i < 10; i++)
            {
                account.Amount += 100;
            }
        });

        threadA.Start();
        threadB.Start();

        threadA.Join();
        threadB.Join();

        account.Amount.Should().Be(initialAmount + 100 * 20);
    }
}