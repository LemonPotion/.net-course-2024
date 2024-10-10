using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class BankService
{
    private readonly List<Person> _blackList;
    private readonly Dictionary<Person, decimal> _bonuses;

    public BankService()
    {
        _blackList = new List<Person>();
        _bonuses = new Dictionary<Person, decimal>();
    }

    public void AddBonus<TPerson>(TPerson person, decimal bonus) 
        where TPerson : Person
    {
        _bonuses[person] += bonus;
    }
    
    public void AddToBlackList<TPerson>(TPerson person) 
        where TPerson : Person
    {
        if (!IsPersonInBlackList(person))
        {
            _blackList.Add(person);
        }
    }
    
    public bool IsPersonInBlackList<TPerson>(TPerson person) 
        where TPerson : Person
    {
        return _blackList.Contains(person);
    }
    
    public decimal CalculateBankOwnersSalary(decimal bankProfit, decimal expenses, int numberOfOwners)
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
            PassportNumber = client.PassportNumber
        };
    }
}