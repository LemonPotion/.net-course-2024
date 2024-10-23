using System.Globalization;
using System.Text.Json;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using CsvHelper;

namespace ExportTool;

public class ExportService <TEntity> where TEntity : class
{
    public void ExportToCsv(IEnumerable<TEntity> entities, string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            using (var streamWriter = new StreamWriter(fileStream))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteHeader<TEntity>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecords(entities);
                }
            }
        }
    }

    public IEnumerable<TEntity> ImportFromCsv(string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.Open))
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var entities = csvReader.GetRecords<TEntity>().ToList();
                    return entities;
                }
            }
        }
    }

    public void SerializeToJson(IEnumerable<TEntity> entities, string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fileStream, entities);
        }
    }
    public void SerializeToJson(TEntity entity, string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fileStream, entity);
        }
    }
    
    public TEntity? DeserializeFromJson(string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.Open))
        {
            return JsonSerializer.Deserialize<TEntity>(fileStream);
        }
    }
    
    public IEnumerable<TEntity>? DeserializeCollectionFromJson(string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.Open))
        {
            return JsonSerializer.Deserialize<IEnumerable<TEntity>>(fileStream);
        }
    }
}