using BankSystem.App.Dto.Currency;
using BankSystem.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyService _currencyService;

    public CurrencyController(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet]
    public async Task<IActionResult> ConvertCurrency([FromQuery] ConvertCurrencyRequest request, CancellationToken cancellationToken)
    {
        var result = await _currencyService.ConvertCurrencyAsync(request, cancellationToken);

        return Ok(result);
    }
}