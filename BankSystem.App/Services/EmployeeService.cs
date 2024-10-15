using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class EmployeeService
{
    private readonly IStorage<Employee> _employeeStorage;

    public EmployeeService(IStorage<Employee> employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }

    public void Add(Employee employee)
    {
        ValidateEmployee(employee);

        _employeeStorage.Add(employee);
    }

    public Employee GetById(Guid id)
    {
        return _employeeStorage.GetById(id);
    }

    public IEnumerable<Employee> GetPaged(int pageNumber, int pageSize, Func<Employee, bool>? filter)
    {
        return _employeeStorage.Get(pageNumber, pageSize, filter);
    }

    public void Update(Employee employee)
    {
        ValidateEmployee(employee);
        _employeeStorage.Update(employee);
    }

    public void Delete(Guid id)
    {
        _employeeStorage.Delete(id);
    }

    private static void ValidateEmployee(Employee employee)
    {
        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        else if (employee.Age < 18)
            throw new AgeRestrictionException(nameof(employee));
        else if (employee.FirstName is null || employee.LastName is null || employee.BirthDay == DateTime.MinValue ||
                 employee.PassportNumber is null)
        {
            throw new PassportDataMissingException(nameof(employee));
        }
    }
}