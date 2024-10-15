namespace BankSystem.Domain.Models;

public class Client : Person
{
    public string BankAccountNumber { get; set; }
    
    public ICollection<Account> Accounts { get; set; }
}