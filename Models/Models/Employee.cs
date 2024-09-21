namespace BankSystem.Domain.Models;

/// <summary>
/// Модель сотрудника.
/// </summary>
public class Employee : Person
{
    /// <summary>
    /// Контракт.
    /// </summary>
    public string Contract { get; set; }
    
    /// <summary>
    /// Зарплата.
    /// </summary>
    public int Salary { get; set; }
    
}