using System.Security.Cryptography;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class ClientService
{
    private readonly IClientStorage _clientStorage;

    public ClientService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }

    public void Add(Client client)
    {
        ValidateClient(client);

        _clientStorage.Add(client);
    }
    
    public List<Client> GetPaged(int pageNumber, int pageSize)
    {
        return _clientStorage.Get(pageNumber, pageSize);
    }

    public void Update(Client client)
    {
        ValidateClient(client);
        _clientStorage.Update(client);
    }

    public void Delete(Client client)
    {
        ValidateClient(client);
        _clientStorage.Delete(client);
    }
    
    public void AddAccount(Client client, Account account)
    {
        ValidateClient(client);
        _clientStorage.AddAccount(client, account);
    }
    
    public void UpdateAccount(Client client, Account account)
    {
        ValidateClient(client);
        _clientStorage.UpdateAccount(client, account);
    }

    public void DeleteAccount(Client client, Account account)
    {
        ValidateClient(client);
        _clientStorage.DeleteAccount(client, account);
    }
    
    private static void ValidateClient(Client client)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        else if (client.Age < 18)
            throw new AgeRestrictionException(nameof(client));
        else  if (client.FirstName is null || client.LastName is null || client.BirthDay == DateTime.MinValue || client.PassportNumber is null)
        {
            throw new PassportDataMissingException(nameof(client));
        }
    }
}