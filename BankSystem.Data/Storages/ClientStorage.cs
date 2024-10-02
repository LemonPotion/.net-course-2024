using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages;

public class ClientStorage
{
    private readonly List<Client> _clients;
    public IEnumerable<Client> Clients => _clients;

    public ClientStorage()
    {
        _clients = new List<Client>();
    }

    public void AddClient(Client client)
    {
        _clients.Add(client);
    }
    
    public void AddRange(IEnumerable<Client> clients)
    {
        _clients.AddRange(clients);
    }

    public Client GetYoungestClient()
    {
        return _clients.MinBy(c => c.Age);
    }

    public Client GetOldestClient()
    {
       return _clients.MaxBy(c => c.Age);
    }

    public double GetAverageClientAge()
    {
        return _clients.Average(c => c.Age);
    }
}