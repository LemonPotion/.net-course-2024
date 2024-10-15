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

    public void Update(Employee item)
    {
        var employee = _bankSystemContext.Employees.Find(item.Id);

        if (employee is null) return;
        
        employee.FirstName = item.FirstName;
        employee.LastName = item.LastName;
        employee.PhoneNumber = item.PhoneNumber;
        employee.Contract = item.Contract;
        employee.BirthDay = item.BirthDay;
        employee.Salary = item.Salary;
        employee.Email = item.Email;
        
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