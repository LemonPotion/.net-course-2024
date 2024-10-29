using BankSystem.App.Interfaces;
using Microsoft.Extensions.Hosting;

namespace BankSystem.App.Services;

public class RateUpdater : BackgroundService
{
    private readonly IClientStorage _clientStorage;
    private readonly TimeSpan _timeSpan;
    private readonly decimal _rate;
    
    public RateUpdater(IClientStorage clientStorage, TimeSpan timeSpan, decimal rate)
    {
        _clientStorage = clientStorage;
        _timeSpan = timeSpan;
        _rate = rate;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateAccountsAsync(stoppingToken);
            await Task.Delay(_timeSpan, stoppingToken);
        }
    }
    
    public async Task UpdateAccountsAsync(CancellationToken cancellationToken)
    {
        var currentTime = DateTime.UtcNow;
        var accounts = await _clientStorage.GetAccountsAsync(1, int.MaxValue, null, cancellationToken);
        
        foreach (var account in accounts.Where(account => (currentTime - account.UpdatedOn).TotalDays >= 30))
        {
            account.Amount *= _rate;
            
            account.UpdatedOn = currentTime;

            await _clientStorage.UpdateAccountAsync(account.Id, account, cancellationToken);
        }
    }
}