namespace BankSystem.Domain.Models;
/// <summary>
/// Структура валюты.
/// </summary>
public struct Currency
{

    /// <summary>
    /// Код валюты (например, USD, EUR).
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Название валюты (например, Доллар США, Евро)
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Символ валюты (например, $, €).
    /// </summary>
    public string Symbol { get; }

    public Currency(string symbol, string name, string code)
    {
        Symbol = symbol;
        Name = name;
        Code = code;
    }
    
}