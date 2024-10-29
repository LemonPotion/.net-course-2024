using BankSystem.App.Interfaces;
using BankSystem.Data.EntityFramework;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storages;

public class EmployeeStorage : IStorage<Employee>
{
    private readonly BankSystemContext _bankSystemContext;

    public EmployeeStorage(BankSystemContext bankSystemContext)
    {
        _bankSystemContext = bankSystemContext;
    }

    public async Task AddAsync(Employee item, CancellationToken cancellationToken)
    {
        await _bankSystemContext.Set<Employee>().AddAsync(item, cancellationToken);

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _bankSystemContext.Employees.FindAsync(id, cancellationToken);
    }

    public async Task<List<Employee>> GetAsync(int pageNumber, int pageSize, Func<Employee, bool>? filter, CancellationToken cancellationToken)
    {
        var items = _bankSystemContext.Employees.AsQueryable();
        if (filter is not null)
        {
            items = items.AsEnumerable().Where(filter).AsQueryable();
        }

        return await items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, Employee employee, CancellationToken cancellationToken)
    {
        var existingEmployee = await _bankSystemContext.Employees.FindAsync(id, cancellationToken);

        if (employee is null) return;

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.PhoneNumber = employee.PhoneNumber;
        existingEmployee.Contract = employee.Contract;
        existingEmployee.BirthDay = employee.BirthDay;
        existingEmployee.Salary = employee.Salary;
        existingEmployee.Email = employee.Email;
        existingEmployee.PassportNumber = employee.PassportNumber;

        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var employee = await GetByIdAsync(id, cancellationToken);

        if (employee is null) return;

        _bankSystemContext.Employees.Remove(employee);
        await _bankSystemContext.SaveChangesAsync(cancellationToken);
    }
    
    public Employee GetYoungestEmployee()
    {
        return _bankSystemContext.Employees.ToList().MinBy(c => c.Age);
    }

    public Employee GetOldestEmployee()
    {
        return _bankSystemContext.Employees.ToList().MaxBy(c => c.Age);
    }

    public double GetAverageEmployeeAge()
    {
        return _bankSystemContext.Employees.ToList().Average(c => c.Age);
    }
}