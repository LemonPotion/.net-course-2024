namespace BankSystem.App.Dto.Currency;

public record ConvertCurrencyRequest(
    string FromCurrency, 
    string ToCurrency, 
    decimal Amount
    );