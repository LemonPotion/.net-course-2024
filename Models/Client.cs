using Models.ValueObjects;

namespace Models;
/// <summary>
/// Модель клиента.
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер банковского счёта.
    /// </summary>
    public string BankAccountNumber { get; set; }
}