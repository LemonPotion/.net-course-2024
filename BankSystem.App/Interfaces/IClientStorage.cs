using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IClientStorage : IStorage<Client>
{
    public void AddAccount(Client client, Account account);
    
    public void UpdateAccount(Client client, Account account);
    
    public void DeleteAccount(Client client, Account account);
}