using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages;

public class ClientStorage
{
    private readonly Dictionary<Client,List<Account>> _clients;
    public Dictionary<Client, List<Account>> Clients => _clients;

    public ClientStorage()
    {
        _clients = new Dictionary<Client, List<Account>>();
    }

    public void AddClient(Client client, List<Account> accounts)
    {
        _clients.Add(client, accounts);
    }
    
    public void AddRange(Dictionary<Client,List<Account>> clientAccountDictionary)
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
    
    public void UpdateClientAccount(Client client,Account originalAccount, Account updatedAccount)
    {
        Clients.TryGetValue(client, out var accounts);

        if (accounts is null) return;
        var existingAccount = accounts.FirstOrDefault(a=> a.Equals(originalAccount));

        if (existingAccount is null) return;
        
        existingAccount.Currency = updatedAccount.Currency;
        existingAccount.Amount = updatedAccount.Amount;
    }

    public Client GetYoungestClient()
    {
        return _clients.Keys.MinBy(c=> c.Age);
    }

    public Client GetOldestClient()
    {
       return _clients.Keys.MaxBy(c=>c.Age);
    }

    public double GetAverageClientAge()
    {
        return _clients.Keys.Average(c=> c.Age);
    }
}