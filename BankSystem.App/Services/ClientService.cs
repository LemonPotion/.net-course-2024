using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class ClientService
{
    private readonly ClientStorage _clientStorage;

    public ClientService(ClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }

    public void AddClient(Client client)
    {
        ValidateClient(client);

        var accounts = new List<Account>
        {
            GetDefaultAccount()
        };

        _clientStorage.AddClient(client, accounts);
    }
    
    public void AddClientRange(IEnumerable<Client> clients)
    {
        var dictionary = new Dictionary<Client, List<Account>>();
        foreach (var client in clients)
        {
            ValidateClient(client);
            var accounts = new List<Account>
            {
                GetDefaultAccount()
            };
            dictionary.Add(client, accounts);
            
        }
        _clientStorage.AddRange(dictionary);
    }
    
    public void AddClientAccount(Client client,Account account)
    {
        ValidateClient(client);
        
        if (_clientStorage.Clients.TryGetValue(client, out var accounts)) 
            accounts.Add(account);
        
    }
    
    public void UpdateClientAccount(Client client,Account account, Account updatedAccount)
    {
        ValidateClient(client);
        _clientStorage.UpdateClientAccount(client, account, updatedAccount);
    }

    public IEnumerable<Client> GetFilteredClients(string? firstName , string? lastName, string? phoneNumber, string? passportNumber, DateTime? startDate, DateTime? endDate)
    {
        var clients = _clientStorage.Clients.Keys.AsQueryable();

        if (!string.IsNullOrWhiteSpace(firstName))
            clients = clients.Where(c => c.FirstName.Contains(firstName));
        
        if (!string.IsNullOrWhiteSpace(lastName))
            clients = clients.Where(c => c.LastName.Contains(lastName));

        if (!string.IsNullOrWhiteSpace(phoneNumber))
            clients = clients.Where(c => c.PhoneNumber.Contains(phoneNumber));
        
        if (!string.IsNullOrWhiteSpace(passportNumber))
            clients = clients.Where(c => c.PassportNumber.Contains(passportNumber));

        if (startDate.HasValue)
            clients = clients.Where(c => c.BirthDay >= startDate.Value);

        if (endDate.HasValue)
            clients = clients.Where(c => c.BirthDay <= endDate.Value);

        return clients;
    }
    
    private static Account GetDefaultAccount()
    {
        return new Account
        {
            Currency = new Currency("$", "United States dollar", "USD"),
            Amount = 0
        };
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