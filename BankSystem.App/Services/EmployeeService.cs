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

    public IEnumerable<Employee> GetFilteredEmployees(string? firstName, string? lastName, string? phoneNumber, string? passportNumber, DateTime? startDate, DateTime? endDate)
    {
        return _employeeStorage.GetFilteredEmployees(firstName, lastName, phoneNumber, passportNumber, startDate, endDate);
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