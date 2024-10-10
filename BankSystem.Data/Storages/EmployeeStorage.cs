﻿using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages;

public class EmployeeStorage : IStorage<Employee>
{
    private readonly List<Employee> _employees;

    public void Add(Employee item)
    {
        _employees.Add(item);
    }
    
    public void AddRange(IEnumerable<Employee> employees)
    {
        _employees.AddRange(employees);
    }
    
    public List<Employee> Get(int pageNumber, int pageSize, Func<Employee, bool>? filter)
    {
       var items = _employees.AsEnumerable();
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
        var employee = _employees.FirstOrDefault(e => e.PassportNumber == item.PassportNumber);

        if (employee is null) return;
        
        employee.FirstName = item.FirstName;
        employee.LastName = item.LastName;
        employee.PhoneNumber = item.PhoneNumber;
        employee.Contract = item.Contract;
        employee.BirthDay = item.BirthDay;
        employee.Salary = item.Salary;
        employee.Email = item.Email;
    }

    public void Delete(Employee item)
    {
        _employees.Remove(item);
    }
    
    public EmployeeStorage()
    {
        _employees = new List<Employee>();
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