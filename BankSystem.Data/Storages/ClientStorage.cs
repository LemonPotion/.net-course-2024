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
}