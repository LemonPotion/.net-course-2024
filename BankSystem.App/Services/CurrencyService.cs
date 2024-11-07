using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using BankSystem.App.Dto.Currency;
using BankSystem.App.Settings;
using Microsoft.Extensions.Options;

namespace BankSystem.App.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken;
    private readonly string _baseUrl;
    
    public CurrencyService(HttpClient httpClient, IOptions<CurrencyApiSettings> currencyApiSettings)
    {
        _httpClient = httpClient;
        _apiToken = currencyApiSettings.Value.Token;
        _baseUrl = currencyApiSettings.Value.BaseUrl;
    }

    public async Task<ConvertCurrencyResponse> ConvertCurrencyAsync(ConvertCurrencyRequest currencyRequest, CancellationToken cancellationToken)
    {
        var uri = GetUri(currencyRequest);
        
        var response = await _httpClient.GetStringAsync(uri, cancellationToken);
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var result = JsonSerializer.Deserialize<ConvertCurrencyResponse>(response, options);

        return result;
    }

    private Uri GetUri(ConvertCurrencyRequest currencyRequest)
    {
        var uriBuilder = new UriBuilder(_baseUrl);
        var query = $"?api_key={_apiToken}&from={currencyRequest.FromCurrency}&to={currencyRequest.ToCurrency}&amount={currencyRequest.Amount}";
        uriBuilder.Query = query;
        
        return uriBuilder.Uri;
    }
}