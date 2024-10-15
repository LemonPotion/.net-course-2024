using BankSystem.App.Interfaces;
using BankSystem.Data.EntityFramework;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storages;

public class ClientStorage : IClientStorage
{
    private readonly BankSystemContext _bankSystemContext;

    public ClientStorage(BankSystemContext bankSystemContext)
    {
        _bankSystemContext = bankSystemContext;
    }
    
    public void Add(Client item)
    {
        _bankSystemContext.Add(item);
        _bankSystemContext.SaveChanges();
        
        var account = GetDefaultAccount();
        account.ClientId = item.Id;

        _bankSystemContext.Add(account);
        
        _bankSystemContext.SaveChanges();
    }
    
    public List<Client> Get(int pageNumber, int pageSize, Func<Client, bool>? filter)
    {
        var items = _bankSystemContext.Clients.AsEnumerable();
    
        if (filter is not null)
        {
            items = items.Where(filter);
        }
        
        return items
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)
            .ToList();
    }
    
    public Client GetById(Guid id)
    {
        return _bankSystemContext.Find<Client>(id);
    }
    
    public void Update(Client item)
    {
        var client = GetById(item.Id);

        if (client is null) return;
        
        client.FirstName = item.FirstName;
        client.LastName = item.LastName;
        client.PhoneNumber = item.PhoneNumber;
        client.BankAccountNumber = item.BankAccountNumber;
        client.Email = item.Email;
        client.BirthDay = item.BirthDay;
            
        _bankSystemContext.SaveChanges();
    }
    
    public void Delete(Guid id)
    {
        var client = _bankSystemContext.Clients.Find(id);
        
        if (client is null) return; 
        
        _bankSystemContext.Remove(client);
        
        _bankSystemContext.SaveChanges();
    }
    

    public void AddAccount(Account account)
    {

        if (GetById(account.ClientId) is null) return;
        
        _bankSystemContext.Accounts.Add(account);
    }

    public Account GetAccountById(Guid accountId)
    {
        return _bankSystemContext.Accounts.Find(accountId);
    }
    
    public List<Account> GetAccounts(Client client, int pageNumber, int pageSize, Func<Account, bool>? filter)
    {
        var accounts = _bankSystemContext.Accounts.AsEnumerable();
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

    public void UpdateAccount(Account account)
    { 
        var originalAccount = _bankSystemContext.Accounts.Find(account.Id);
        
        if (originalAccount is null) return;
        
        originalAccount.Currency = account.Currency;
        originalAccount.Amount = account.Amount;

        _bankSystemContext.Update(originalAccount);

        _bankSystemContext.SaveChanges();
    }

    public void DeleteAccount(Guid accountId)
    {
        var account = _bankSystemContext.Accounts.Find(accountId);
        
        if (account is null) return; 
        
        _bankSystemContext.Remove(account);

        _bankSystemContext.SaveChanges();
    }
}