using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class EmployeeService
{
     private readonly EmployeeStorage _employeeStorage;

    public EmployeeService(EmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }

    public void AddEmployee(Employee employee)
    {
        ValidateEmployee(employee);

        _employeeStorage.AddEmployee(employee);
    }
    
    public void AddEmployeeRange(IEnumerable<Employee> employees)
    {
        _employeeStorage.AddRange(employees);
    }
    
    public void UpdateEmployee(Employee employee, Employee updatedEmployee)
    {
        ValidateEmployee(employee);
        ValidateEmployee(updatedEmployee);
        _employeeStorage.UpdateEmployee(employee, updatedEmployee);
    }

    public IEnumerable<Employee> GetFilteredEmployees(string? firstName , string? lastName, string? phoneNumber, string? passportNumber, DateTime? startDate, DateTime? endDate)
    {
        var employees = _employeeStorage.Employees.AsQueryable();

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
    
    private static void ValidateEmployee(Employee employee)
    {
        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        else if (employee.Age < 18)
            throw new AgeRestrictionException(nameof(employee));
        else  if (employee.FirstName is null || employee.LastName is null || employee.BirthDay == DateTime.MinValue || employee.PassportNumber is null)
        {
            throw new PassportDataMissingException(nameof(employee));
        }
    }
}