using System.Diagnostics;
using BankSystem.Domain.Models;
using BankSystem.App.Services;
namespace Practice;

class Program
{
    static void Main(string[] args)
    {
        RefAndValueType();
        ListAndDictionary();
    }

    private static void ListAndDictionary()
    {
        var testDataGenerator = new TestDataGenerator();
        var stopwatch = new Stopwatch();
        
        var clientList = testDataGenerator.GenerateClients();
        stopwatch.Start();
        var clientFromList = clientList.FirstOrDefault(c => c.PhoneNumber == clientList[115].PhoneNumber);
        stopwatch.Stop();
        Console.WriteLine($"Найден сотрудник: {clientFromList.Firstname} по номеру :{clientFromList.PhoneNumber} за {stopwatch.Elapsed}");
        
        var clientDictionary = testDataGenerator.GenerateClientsDictionary();
        var key = clientDictionary.ElementAt(115).Key; 
        stopwatch.Restart();
        var clientFromDictionary = clientDictionary[key];
        stopwatch.Stop();
        Console.WriteLine($"Найден сотрудник: {clientFromDictionary.Firstname} по номеру: {clientFromDictionary.PhoneNumber} за {stopwatch.Elapsed}");
        
        var filteredClientsList = clientList.Where(c => c.Age < 1).ToList();
        Console.WriteLine($"Надено {filteredClientsList.Count} клиентов");

        var employeeList = testDataGenerator.GenerateEmployees();
        var minSalary = employeeList.Min(e => e.Salary);
        Console.WriteLine($"Минимальная зарплата сотрудника: {minSalary}");
        
        stopwatch.Restart();
        var lastOrDefaultClient = clientDictionary.LastOrDefault();
        stopwatch.Stop();
        Console.WriteLine($"Поиск с LastOrDefault: {stopwatch.Elapsed}");
        stopwatch.Restart();
        var lastClient = clientDictionary[lastOrDefaultClient.Key];
        stopwatch.Stop();
        Console.WriteLine($"Поиск по ключу: {stopwatch.Elapsed}");

    }

    public static void RefAndValueType()
    {

        var employee = new Employee()
        {
            Email = "e@mail.com",
            Contract = "Boring contract"
        };
        Console.WriteLine(employee.Contract); 
        UpdateEmployeeContract(employee);
        Console.WriteLine(employee.Contract);
        
        var currency = CreateCurrency("$", "US Dollar", "USD");
        Console.WriteLine($"{currency.Symbol} {currency.Name} {currency.Code}");
        UpdateCurrency(ref currency);
        Console.WriteLine($"{currency.Symbol} {currency.Name} {currency.Code}");
        

        var bankService = new BankService();

        var salary = bankService.CalculateBankOwnersSalary(1000, 1, 23);
        Console.WriteLine($"Зарплата владельцев банка:{salary}");
        
        var client = new Client()
        {
            Email = "e@mail.com",
            BankAccountNumber = 123,
        };
        var convertedEmployee = bankService.ConvertClientToEmployee(client);
        Console.WriteLine(convertedEmployee.Email);
        Console.WriteLine(convertedEmployee.Contract);
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