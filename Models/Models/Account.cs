namespace BankSystem.Domain.Models;

public class Account
{
    public Guid Id { get; set; }
    
    public int Amount { get; set; }
    
    public Currency Currency { get; set; }
    
    public Guid CurrencyId { get; set; }
    
    public Client Client { get; set; }
    
    public Guid ClientId { get; set; }
}