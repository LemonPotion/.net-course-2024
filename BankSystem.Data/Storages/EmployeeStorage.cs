using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages;

public class EmployeeStorage
{
    private readonly List<Employee> _employees;
    public IEnumerable<Employee> Employees => _employees;

    public EmployeeStorage()
    {
        _employees = new List<Employee>();
    }

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
    }
    
    public void AddRange(IEnumerable<Employee> employees)
    {
        _employees.AddRange(employees);
    }
    
    public Employee GetYoungestEmployee()
    {
        return _employees.MinBy(c => c.Age);
    }

    public Employee GetOldestEmployee()
    {
        return _employees.MaxBy(c => c.Age);
    }

    public double GetAverageEmployeeAge()
    {
        return _employees.Average(c => c.Age);
    }
}