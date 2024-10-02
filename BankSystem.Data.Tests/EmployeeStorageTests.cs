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
        storage.AddRange(employees);
        
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
        storage.AddRange(employees);
        
        //Act
        var youngestEmployee = storage.GetYoungestEmployee();
        var expectedYoungestEmployee = employees.MinBy(c => c.Age);
        
        //Assert
        youngestEmployee.Should().BeEquivalentTo(expectedYoungestEmployee);
    }
    
    [Fact]
    public void EmployeeStorageGetOldestEmployeeReturnsOldestEmployee()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new EmployeeStorage();
        var employees = dataGenerator.GenerateEmployees();
        storage.AddRange(employees);
        
        //Act
        var oldestEmployee = storage.GetOldestEmployee();
        var expectedOldestEmployee = employees.MaxBy(c => c.Age);
        
        //Assert
        oldestEmployee.Should().BeEquivalentTo(expectedOldestEmployee);
    }
    
    [Fact]
    public void EmployeeStorageGetAverageEmployeeAgeReturnsAverageEmployeeAge()
    {
        //Arrange
        var dataGenerator = new TestDataGenerator();
        var storage = new EmployeeStorage();
        var employees = dataGenerator.GenerateEmployees();
        storage.AddRange(employees);
        
        //Act
        var averageAge = storage.GetAverageEmployeeAge();
        var expectedAverageAge = employees.Average(c => c.Age);
        
        //Assert
        averageAge.Should().Be(expectedAverageAge);
    }
}