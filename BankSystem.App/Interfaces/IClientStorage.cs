using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IClientStorage : IStorage<Client>
{
    public void AddAccount(Account account);

    public void UpdateAccount(Account account);

    public void DeleteAccount(Guid accountId);

    public Account GetAccountById(Guid accountId);

    public List<Account> GetAccounts(int pageNumber, int pageSize, Func<Account, bool>? filter);
}