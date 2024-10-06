using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class BankService
{
    public decimal CalculateBankOwnersSalary(decimal bankProfit,decimal expenses, int numberOfOwners)
    {
        return (bankProfit - expenses) / numberOfOwners;
    }
    
    public Employee ConvertClientToEmployee(Client client)
    {
        return new Employee
        {
            BirthDay = client.BirthDay,
            FirstName = client.FirstName,
            LastName = client.LastName,
            PhoneNumber = client.PhoneNumber,
            Email = client.Email,
        };
    }
    
}