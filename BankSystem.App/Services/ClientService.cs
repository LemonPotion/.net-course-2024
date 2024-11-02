using System.Linq.Expressions;
using AutoMapper;
using BankSystem.App.Dto.Client.Requests;
using BankSystem.App.Dto.Client.Responses;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class ClientService
{
    private readonly IClientStorage _clientStorage;
    private readonly IMapper _mapper;

    public ClientService(IClientStorage clientStorage, IMapper mapper)
    {
        _clientStorage = clientStorage;
        _mapper = mapper;
    }

    public async Task WithdrawFundsAsync(Guid accountId, decimal amount, CancellationToken cancellationToken)
    {
        var account = await _clientStorage.GetAccountByIdAsync(accountId, cancellationToken);

        if (account != null && account.Amount >= amount)
        {
            account.Amount -= amount;
            account.UpdatedOn = DateTime.UtcNow;
            await _clientStorage.UpdateAccountAsync(account.Id, account, cancellationToken);
        }
    }

    public async Task AddAsync(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Client>(request);
        
        ValidateClient(client);

        await _clientStorage.AddAsync(client, cancellationToken);
    }

    public async Task<IEnumerable<GetClientByIdResponse>> GetPagedAsync(GetAllClientsPagedRequest request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<Expression<Func<Client, bool>>>(request.Filter);
        var clients = await _clientStorage.GetAsync(request.Pagination.PageNumber, request.Pagination.PageSize, filter, cancellationToken);

        return _mapper.Map<List<GetClientByIdResponse>>(clients);
    }

    public async Task<GetClientByIdResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await _clientStorage.GetByIdAsync(id, cancellationToken);

        return _mapper.Map<GetClientByIdResponse>(client);
    }

    public async Task UpdateAsync(UpdateClientRequest request,CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Client>(request);
        
        ValidateClient(client);
        await _clientStorage.UpdateAsync(client.Id, client, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await _clientStorage.GetByIdAsync(id, cancellationToken);
        ValidateClient(client);
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

    public async Task<List<Account>> GetAccountsPagedAsync(int pageNumber, int pageSize, Guid clientId, Expression<Func<Account, bool>>? filter, CancellationToken cancellationToken)
    {
        var client = await _clientStorage.GetByIdAsync(clientId, cancellationToken);
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