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

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        ValidateEmployee(employee);

        await _employeeStorage.AddAsync(employee, cancellationToken);
    }

    public async Task<Employee> GetByIdASync(Guid id, CancellationToken cancellationToken)
    {
        return await _employeeStorage.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetPagedAsync(int pageNumber, int pageSize, Func<Employee, bool>? filter, CancellationToken cancellationToken)
    {
        return await _employeeStorage.GetAsync(pageNumber, pageSize, filter, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, Employee employee, CancellationToken cancellationToken)
    {
        ValidateEmployee(employee);
        await _employeeStorage.UpdateAsync(id, employee, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _employeeStorage.DeleteAsync(id, cancellationToken);
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