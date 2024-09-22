using BankSystem.Domain.Models;
using BankSystem.App.Services;
namespace Practice;

class Program
{
    static void Main(string[] args)
    {
        FirstHomework();
    }
    
    /// <summary>
    /// Первая домашка
    /// </summary>
    public static void FirstHomework()
    {
        
        //Задание 1.
        //a
        var employee = CreateNewEmployee();
        Console.WriteLine(employee.Contract); 
        UpdateEmployeeContract(employee);
        Console.WriteLine(employee.Contract);
        
        //b
        var currency = CreateCurrency("$", "US Dollar", "USD");
        Console.WriteLine($"{currency.Symbol} {currency.Name} {currency.Code}");
        UpdateCurrency(ref currency);
        Console.WriteLine($"{currency.Symbol} {currency.Name} {currency.Code}");
        
        //Задание 2.
        
        var bankService = new BankService();
        //a
        var salary = bankService.CalculateBankOwnersSalary(1000,1,23);
        Console.WriteLine($"Зарплата владельцев банка:{salary}");
        
        //b
        var client = CreateNewClient();
        var convertedEmployee = bankService.ConvertClientToEmployee(client);
        Console.WriteLine(convertedEmployee.Email);
        Console.WriteLine(convertedEmployee.Contract);
    }
    
    public static Employee CreateNewEmployee()
    {
        return new Employee()
        {
            Email = "e@mail.com",
            Contract = "Boring contract"
        };
    }
    
    public static Client CreateNewClient()
    {
        return new Client()
        {
            Email = "e@mail.com",
            BankAccountNumber = "123",
        };
    }
    
    public static void UpdateEmployeeContract(Employee employee)
    {
        employee.Contract = $"Devil's deal {employee.Email}";
    }
    
    public static Currency CreateCurrency(string symbol, string name , string code)
    {
        return new Currency(symbol, name, code);
    }
    
    public static void UpdateCurrency(ref Currency currency)
    {
        currency = CreateCurrency("Э", "Euro", "EUR");
    }
}