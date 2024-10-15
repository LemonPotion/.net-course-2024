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

    public void Add(Client client)
    {
        var account = GetDefaultAccount();

        _bankSystemContext.Clients.Add(client);
        account.ClientId = client.Id;
        _bankSystemContext.Accounts.Add(account);

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

    public void Update(Client employee)
    {
        var existingclient = GetById(employee.Id);

        if (employee is null) return;

        existingclient.FirstName = employee.FirstName;
        existingclient.LastName = employee.LastName;
        existingclient.PhoneNumber = employee.PhoneNumber;
        existingclient.BankAccountNumber = employee.BankAccountNumber;
        existingclient.Email = employee.Email;
        existingclient.BirthDay = employee.BirthDay;
        existingclient.PassportNumber = employee.PassportNumber;

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

        _bankSystemContext.SaveChanges();
    }

    public Account GetAccountById(Guid accountId)
    {
        return _bankSystemContext.Accounts.Find(accountId);
    }

    public List<Account> GetAccounts(int pageNumber, int pageSize, Func<Account, bool>? filter)
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

    private Account GetDefaultAccount()
    {
        var currency = _bankSystemContext.Currencies.FirstOrDefault(c => c.Code == "USD");

        if (currency == null)
        {
            currency = new Currency("$", "United States dollar", "USD");
            _bankSystemContext.Currencies.Add(currency);
            _bankSystemContext.SaveChanges();
        }

        return new Account
        {
            Currency = currency,
            Amount = 0
        };
    }

    public void UpdateAccount(Account account)
    {
        var originalAccount = _bankSystemContext.Accounts.Find(account.Id);

        if (originalAccount is null) return;

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