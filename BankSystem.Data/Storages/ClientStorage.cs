using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages;

public class ClientStorage : IClientStorage
{
    private readonly Dictionary<Client, List<Account>> _clients;

    public ClientStorage()
    {
        _clients = new Dictionary<Client, List<Account>>();
    }
    
    public void Add(Client item)
    {
        var accounts = new List<Account>
        {
            GetDefaultAccount()
        };
        _clients.Add(item, accounts);
    }

    public void AddRange(Dictionary<Client, List<Account>> clientAccountDictionary)
    {
        foreach (var item in clientAccountDictionary)
        {
            if (!_clients.TryGetValue(item.Key, out var client))
            {
                _clients.Add(item.Key, item.Value);
            }
            else
            {
                client.AddRange(item.Value);
            }
        }
    }
    
    public List<Client> Get(int pageNumber, int pageSize, Func<Client, bool>? filter)
    {
        var items = _clients.Keys.AsEnumerable();
        if (filter is not null)
        {
            items = items.Where(filter);
        }
        
        return items
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)
            .ToList();
    }
    
    public void Update(Client item)
    {
        var client = _clients.Keys.FirstOrDefault(c => c.PassportNumber == item.PassportNumber);

        if (client is null) return;
        
        client.FirstName = item.FirstName;
        client.LastName = item.LastName;
        client.PhoneNumber = item.PhoneNumber;
        client.BankAccountNumber = item.BankAccountNumber;
        client.Email = item.Email;
        client.BirthDay = item.BirthDay;
    }
    
    public void Delete(Client item)
    {
        _clients.Remove(item);
    }
    
    public Client GetYoungestClient()
    {
        return _clients.Keys.MinBy(c => c.Age);
    }

    public Client GetOldestClient()
    {
        return _clients.Keys.MaxBy(c => c.Age);
    }

    public double GetAverageClientAge()
    {
        return _clients.Keys.Average(c => c.Age);
    }

    public void AddAccount(Client client, Account account)
    {
        var accounts = _clients[client];
        
        if (accounts is null) return; 
        
        accounts.Add(account);
    }
    
    public List<Account> GetAccounts(Client client, int pageNumber, int pageSize, Func<Account, bool>? filter)
    {
        var accounts = _clients[client].AsEnumerable();
        if (filter is not null)
        {
            accounts = accounts.Where(filter);
        }
        return accounts
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)
            .ToList();
    }
    
    private static Account GetDefaultAccount()
    {
        return new Account
        {
            Currency = new Currency("$", "United States dollar", "USD"),
            Amount = 0
        };
    }

    public void UpdateAccount(Client client, Account account)
    { 
        var accounts = _clients[client];
        
        if (accounts is null) return;

        var clientAccount = accounts.FirstOrDefault(a => a.Currency.Equals(account.Currency));

        if (clientAccount is null) return;
        
        clientAccount.Currency = account.Currency;
        clientAccount.Amount = account.Amount;
    }

    public void DeleteAccount(Client client, Account account)
    {
        var accounts = _clients[client];
        
        if (accounts is null) return;
        
        var clientAccount = accounts.Remove(account);
    }
}