namespace BankSystem.Domain.Models;

public class Currency
{
    public Guid Id { get; set; }
    public string Code { get; set; }

    public string Name { get; set; }

    public string Symbol { get; set; }
    
    public ICollection<Account> Accounts { get; set; }

    public Currency(string symbol, string name, string code)
    {
        Symbol = symbol;
        Name = name;
        Code = code;
    }
}