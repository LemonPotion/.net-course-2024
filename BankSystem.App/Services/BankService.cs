using BankSystem.Domain.Models;

namespace BankSystem.App.Services;
/// <summary>
/// Сервис банка.
/// </summary>
public class BankService
{
    /// <summary>
    /// Метод для рассчета зарплаты владельцев банка.
    /// </summary>
    /// <param name="bankProfit">Прибыль банка.</param>
    /// <param name="expenses">Расходы банка.</param>
    /// <param name="numberOfOwners">Количество владельцев.</param>
    /// <returns>Зарплату владельцев банка.</returns>
    public int CalculateBankOwnersSalary(int bankProfit,int expenses, int numberOfOwners)
    {
        return (bankProfit - expenses) / numberOfOwners;
    }
    
    /// <summary>
    /// Метод для конвертации Client в Employee.
    /// </summary>
    /// <param name="client">Обьект клиента банка.</param>
    /// <returns>Новый сотрудник банка.</returns>
    public Employee ConvertClientToEmployee(Client client)
    {
        return new Employee
        {
            FullName = client.FullName,
            BirthDay = client.BirthDay,
            PhoneNumber = client.PhoneNumber,
            Email = client.Email,
        };
    }
    
}