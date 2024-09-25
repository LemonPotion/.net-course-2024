namespace BankSystem.Domain.Models;

/// <summary>
/// Модель сотрудника.
/// </summary>
public class Employee : Person
{
    public string Contract { get; set; }
    
    public decimal Salary { get; set; }
    
}