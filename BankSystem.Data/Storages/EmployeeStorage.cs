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

    public void Add(Employee item)
    {
        _bankSystemContext.Set<Employee>().Add(item);

        _bankSystemContext.SaveChanges();
    }

    public Employee GetById(Guid id)
    {
        return _bankSystemContext.Employees.Find(id);
    }

    public List<Employee> Get(int pageNumber, int pageSize, Func<Employee, bool>? filter)
    {
        var items = _bankSystemContext.Employees.AsEnumerable();
        if (filter is not null)
        {
            items = items.Where(filter);
        }

        return items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public void Update(Employee employee)
    {
        var existingEmployee = _bankSystemContext.Employees.Find(employee.Id);

        if (employee is null) return;

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.PhoneNumber = employee.PhoneNumber;
        existingEmployee.Contract = employee.Contract;
        existingEmployee.BirthDay = employee.BirthDay;
        existingEmployee.Salary = employee.Salary;
        existingEmployee.Email = employee.Email;
        existingEmployee.PassportNumber = employee.PassportNumber;

        _bankSystemContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var employee = GetById(id);

        if (employee is null) return;

        _bankSystemContext.Employees.Remove(employee);
        _bankSystemContext.SaveChanges();
    }
}