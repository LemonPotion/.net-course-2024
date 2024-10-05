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

    public void UpdateEmployee(Employee employee, Employee updatedEmployee)
    {
        var originalEmployee = _employees.FirstOrDefault(e=> e.Equals(employee));

        if (originalEmployee is null) return;
        
        originalEmployee.FirstName = updatedEmployee.FirstName;
        originalEmployee.LastName = updatedEmployee.LastName;
        originalEmployee.Contract = updatedEmployee.Contract;
        originalEmployee.BirthDay = updatedEmployee.BirthDay;
        originalEmployee.Salary = updatedEmployee.Salary;
        originalEmployee.Email = updatedEmployee.Email;
        originalEmployee.PassportNumber = updatedEmployee.PassportNumber;
        originalEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
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