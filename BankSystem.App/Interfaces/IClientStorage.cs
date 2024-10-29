using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IClientStorage : IStorage<Client>
{
    public Task AddAccountAsync(Account account, CancellationToken cancellationToken);

    public Task UpdateAccountAsync(Guid id, Account account, CancellationToken cancellationToken);

    public Task DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken);

    public Task<Account> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken);

    public Task<List<Account>> GetAccountsAsync(int pageNumber, int pageSize, Func<Account, bool>? filter, CancellationToken cancellationToken);
}