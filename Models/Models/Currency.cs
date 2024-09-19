namespace BankSystem.Domain.Models;

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

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }
        else if (obj is not Currency currency)
        {
            return false;
        }
        else if (currency.Code != Code && currency.Symbol != Symbol && currency.Name != Name)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"{nameof(Currency)} : Name= {Name}, Symbol= ({Symbol}), Code= {Code}"; ;
    }
}