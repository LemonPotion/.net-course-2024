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
}