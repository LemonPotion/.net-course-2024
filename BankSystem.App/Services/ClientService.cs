using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class ClientService
{
    private readonly IClientStorage _clientStorage;

    public ClientService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }

    public async Task WithdrawFunds(Dictionary<Guid, decimal> withdrawalRequests, CancellationToken cancellationToken)
    {
        var tasks = withdrawalRequests.Select(async request =>
        {
            var account = await _clientStorage.GetAccountByIdAsync(request.Key, cancellationToken);

            if (account.Amount >= request.Value)
            {
                account.Amount -= request.Value;
                account.UpdatedOn = DateTime.UtcNow;
                await _clientStorage.UpdateAccountAsync(account.Id, account, cancellationToken);
            }
        }).ToList();

        await Task.WhenAll(tasks);
    }

    public async Task AddAsync(Client client, CancellationToken cancellationToken)
    {
        ValidateClient(client);

        await _clientStorage.AddAsync(client, cancellationToken);
    }

    public async Task<List<Client>> GetPagedAsync(int pageNumber, int pageSize, Func<Client, bool>? filter, CancellationToken cancellationToken)
    {
        return await _clientStorage.GetAsync(pageNumber, pageSize, filter, cancellationToken);
    }

    public async Task<Client> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _clientStorage.GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Guid id,Client client, CancellationToken cancellationToken)
    {
        ValidateClient(client);
        await _clientStorage.UpdateAsync(id, client, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
         ValidateClient(await GetByIdAsync(id, cancellationToken));
        await _clientStorage.DeleteAsync(id, cancellationToken);
    }

    public async Task AddAccountAsync(Account account, CancellationToken cancellationToken)
    {
        await _clientStorage.AddAccountAsync(account, cancellationToken);
    }

    public async Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _clientStorage.GetAccountByIdAsync(id, cancellationToken);
    }

    public async Task<List<Account>> GetAccountsPagedAsync(int pageNumber, int pageSize, Guid clientId, Func<Account, bool>? filter, CancellationToken cancellationToken)
    {
        var client = await GetByIdAsync(clientId, cancellationToken);
        ValidateClient(client);
        return await _clientStorage.GetAccountsAsync(pageNumber, pageSize, filter, cancellationToken);
    }

    public async Task UpdateAccountAsync(Guid id, Account account, CancellationToken cancellationToken)
    {
        await _clientStorage.UpdateAccountAsync(id, account, cancellationToken);
    }

    public async Task DeleteAccountAsync(Guid id, CancellationToken cancellationToken)
    {
        await _clientStorage.DeleteAccountAsync(id, cancellationToken);
    }

    private static void ValidateClient(Client client)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        else if (client.Age < 18)
            throw new AgeRestrictionException(nameof(client));
        else if (client.PassportNumber is null)
        {
            throw new PassportDataMissingException(nameof(client));
        }
    }
}