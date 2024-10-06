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
    
    public IEnumerable<Employee> GetFilteredEmployees(string? firstName, string? lastName, string? phoneNumber, string? passportNumber, DateTime? startDate, DateTime? endDate)
    {
        var employees = GetAll().AsQueryable();

        if (!string.IsNullOrWhiteSpace(firstName))
            employees = employees.Where(c => c.FirstName.Contains(firstName));
        
        if (!string.IsNullOrWhiteSpace(lastName))
            employees = employees.Where(c => c.LastName.Contains(lastName));

        if (!string.IsNullOrWhiteSpace(phoneNumber))
            employees = employees.Where(c => c.PhoneNumber.Contains(phoneNumber));
        
        if (!string.IsNullOrWhiteSpace(passportNumber))
            employees = employees.Where(c => c.PassportNumber.Contains(passportNumber));

        if (startDate.HasValue)
            employees = employees.Where(c => c.BirthDay >= startDate.Value);

        if (endDate.HasValue)
            employees = employees.Where(c => c.BirthDay <= endDate.Value);

        return employees;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employees;
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