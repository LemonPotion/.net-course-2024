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

    public List<Client> GetPaged(int pageNumber, int pageSize, Func<Client, bool>? filter)
    {
        return _clientStorage.Get(pageNumber, pageSize, filter);
    }

    public Client GetById(Guid id)
    {
        return _clientStorage.GetById(id);
    }

    public void Update(Guid id,Client client)
    {
        ValidateClient(client);
        _clientStorage.Update(id, client);
    }

    public void Delete(Guid id)
    {
        ValidateClient(GetById(id));
        _clientStorage.Delete(id);
    }

    public void AddAccount(Account account)
    {
        _clientStorage.AddAccount(account);
    }

    public Account GetAccountById(Guid id)
    {
        return _clientStorage.GetAccountById(id);
    }

    public List<Account> GetAccountsPaged(int pageNumber, int pageSize, Guid clientId, Func<Account, bool>? filter)
    {
        var client = GetById(clientId);
        ValidateClient(client);
        return _clientStorage.GetAccounts(pageNumber, pageSize, filter);
    }

    public void UpdateAccount(Guid id, Account account)
    {
        _clientStorage.UpdateAccount(id, account);
    }

    public void DeleteAccount(Guid id)
    {
        _clientStorage.DeleteAccount(id);
    }

    private static void ValidateClient(Client client)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        else if (client.Age < 18)
            throw new AgeRestrictionException(nameof(client));
        else if (client.PassportNumber is null)
        {
            throw new PassportDataMissingException(nameof(client));
        }
    }
}