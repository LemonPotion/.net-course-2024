using System.Linq.Expressions;
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

    public async Task AddAsync(Client client, CancellationToken cancellationToken)
    {
        var account = await GetDefaultAccount(cancellationToken);

        await _bankSystemContext.Clients.AddAsync(client, cancellationToken);
        account.ClientId = client.Id;
        await _bankSystemContext.Accounts.AddAsync(account, cancellationToken);

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Client>> GetAsync(int pageNumber, int pageSize, Expression<Func<Client, bool>>? filter, CancellationToken cancellationToken)
    {
        var items = _bankSystemContext.Clients.AsQueryable();

        if (filter is not null)
        {
            items = items.Where(filter);
        }

        return await items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Client> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _bankSystemContext.FindAsync<Client>(id, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, Client employee, CancellationToken cancellationToken)
    {
        var existingClient = await GetByIdAsync(employee.Id, cancellationToken);

        if (employee is null) return;

        existingClient.FirstName = employee.FirstName;
        existingClient.LastName = employee.LastName;
        existingClient.PhoneNumber = employee.PhoneNumber;
        existingClient.BankAccountNumber = employee.BankAccountNumber;
        existingClient.Email = employee.Email;
        existingClient.BirthDay = employee.BirthDay;
        existingClient.PassportNumber = employee.PassportNumber;

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await _bankSystemContext.Clients.FindAsync(id, cancellationToken);

        if (client is null) return;

        _bankSystemContext.Remove(client);

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }


    public async Task AddAccountAsync(Account account, CancellationToken cancellationToken)
    {
        if (GetByIdAsync(account.ClientId, cancellationToken) is null) return;

        await _bankSystemContext.Accounts.AddAsync(account, cancellationToken);

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Account> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await _bankSystemContext.Accounts.FindAsync(accountId, cancellationToken);
    }

    public async Task<List<Account>> GetAccountsAsync(int pageNumber, int pageSize, Expression<Func<Account, bool>>? filter, CancellationToken cancellationToken)
    {
        var accounts = _bankSystemContext.Accounts.AsQueryable();
        if (filter is not null)
        {
            accounts = accounts.Where(filter);
        }

        return await accounts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    private async Task<Account> GetDefaultAccount(CancellationToken cancellationToken)
    {
        var currency = await _bankSystemContext.Currencies.FirstOrDefaultAsync(c => c.Code == "USD", cancellationToken);

        if (currency == null)
        {
            currency = new Currency("$", "United States dollar", "USD");
            _bankSystemContext.Currencies.Add(currency);
            await _bankSystemContext.SaveChangesAsync(cancellationToken);
        }

        return new Account
        {
            Currency = currency,
            Amount = 0
        };
    }

    public async Task UpdateAccountAsync(Guid id, Account account, CancellationToken cancellationToken)
    {
        var originalAccount = await _bankSystemContext.Accounts.FindAsync(account.Id, cancellationToken);

        if (originalAccount is null) return;

        originalAccount.Amount = account.Amount;

        _bankSystemContext.Update(originalAccount);

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _bankSystemContext.Accounts.FindAsync(
            accountId, cancellationToken);

        if (account is null) return;

        _bankSystemContext.Remove(account);

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }
    
    public Client GetYoungestClient()
    {
        return _bankSystemContext.Clients.ToList().MinBy(c => c.Age);
    }

    public Client GetOldestClient()
    {
        return _bankSystemContext.Clients.ToList().MaxBy(c => c.Age);
    }

    public double GetAverageClientAge()
    {
        return _bankSystemContext.Clients.ToList().Average(c => c.Age);
    }
}