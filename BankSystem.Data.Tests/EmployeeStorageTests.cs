using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class EmployeeStorageTests
{
   [Fact]
    public void EmployeeStorageAddEmployeeToStorageShouldAddSuccessfully()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new EmployeeStorage();
        var employees = dataGenerator.GenerateEmployees();
        
        //Act
        foreach (var employee in employees )
        {
            storage.AddEmployee(employee);
        }
        
        //Assert
        storage.Employees.Should().BeEquivalentTo(employees);
    }

    [Fact]
    public void EmployeeStorageGetYoungestEmployeeReturnsYoungestEmployee()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new EmployeeStorage();
        var employees = dataGenerator.GenerateEmployees();
        foreach (var employee in employees )
        {
            storage.AddEmployee(employee);
        }
        
        //Act
        var youngestEmployee = storage.Employees.MinBy(c => c.Age);
        var expectedYoungestEmployee = employees.MinBy(c => c.Age);
        
        //Assert
        youngestEmployee.Should().BeOfType<Employee>();
        youngestEmployee.Should().BeEquivalentTo(expectedYoungestEmployee);
    }
    
    [Fact]
    public void EmployeeStorageGetOldestEmployeeReturnsOldestEmployee()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new EmployeeStorage();
        var employees = dataGenerator.GenerateEmployees();
        foreach (var employee in employees )
        {
            storage.AddEmployee(employee);
        }
        
        //Act
        var oldestEmployee = storage.Employees.MaxBy(c => c.Age);
        var expectedOldestEmployee = employees.MaxBy(c => c.Age);
        
        //Assert
        oldestEmployee.Should().BeOfType<Employee>();
        oldestEmployee.Should().BeEquivalentTo(expectedOldestEmployee);
    }
    
    [Fact]
    public void EmployeeStorageGetAverageEmployeeAgeReturnsAverageEmployeeAge()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new EmployeeStorage();
        var employees = dataGenerator.GenerateEmployees();
        foreach (var employee in employees )
        {
            storage.AddEmployee(employee);
        }
        
        //Act
        var averageAge = storage.Employees.Average(c => c.Age);
        var expectedAverageAge = employees.Average(c => c.Age);
        
        //Assert
        averageAge.Should().Be(expectedAverageAge);
    }
}