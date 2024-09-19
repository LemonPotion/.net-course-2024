using Models;
using Models.ValueObjects;

namespace PracticeWithTypes;

class Program
{
    static void Main(string[] args)
    {
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
    }
    public static Employee CreateNewEmployee()
    {
        return new Employee()
        {
            Email = "e@mail.com",
            Contract = "Boring contract"
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
        currency = CreateCurrency("Э", "Euro", "EUR");  // Изменение оригинального объекта
    }
}