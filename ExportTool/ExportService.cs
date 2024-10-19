using System.Globalization;
using BankSystem.App.Interfaces;
using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using CsvHelper;

namespace ExportTool;

public class ExportService
{
    private readonly IClientStorage _clientStorage;

    public ExportService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }
    
    
    public void ExportClientsData(IEnumerable<Client> clients, string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            using (var streamWriter = new StreamWriter(fileStream))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(clients);
                }
            }
        }
    }

    public List<Client> ImportClientsData(string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var clients = csvReader.GetRecords<Client>().ToList();
                    foreach (var client in clients)
                    {
                        client.BirthDay = client.BirthDay.ToUniversalTime();
                        _clientStorage.Add(client);
                    }

                    return clients;
                }
            }
        }
    }
}