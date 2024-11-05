namespace BankSystem.App.Dto.Currency;

public record ConvertCurrencyResponse(
    int Error,
    string ErrorMessage,
    decimal Amount
    );