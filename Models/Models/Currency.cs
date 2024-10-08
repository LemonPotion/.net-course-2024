namespace BankSystem.Domain.Models;

public struct Currency
{
    public string Code { get; }

    public string Name { get; }

    public string Symbol { get; }

    public Currency(string symbol, string name, string code)
    {
        Symbol = symbol;
        Name = name;
        Code = code;
    }
}