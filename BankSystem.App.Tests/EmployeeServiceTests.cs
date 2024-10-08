using BankSystem.App.Services;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.App.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public void EmployeeServiceAddEmployeeShouldAddEmployee()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        
        var employees = testDataGenerator.GenerateEmployees();
        
        //Act
        employeeService.Add(employees.First());

        //Assert
        employeeStorage.Employees.Should().NotBeNull();
    }
    
    [Fact]
    public void EmployeeServiceGetPagedReturnsPagedEmployees()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        
        var employees = testDataGenerator.GenerateEmployees();
        foreach (var item in employees)
        {
            employeeService.Add(item);
        }

        //Act
        var filteredEmployees = employeeService.GetPaged(1, 10);

        //Assert
        filteredEmployees.Should().NotBeNull();
    }
    
    [Fact]
    public void EmployeeServiceUpdateEmployeeShouldUpdateEmployee()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        
        var employees = testDataGenerator.GenerateEmployees();
        var originalEmployee = employees.First();
        var updatedEmployee = employees.Last();
        updatedEmployee.PassportNumber = originalEmployee.PassportNumber;
        
        employeeService.Add(originalEmployee);
        
        //Act
        employeeService.Update(updatedEmployee);
        
        //Assert
        originalEmployee.Should().BeEquivalentTo(updatedEmployee);
    }
    
    [Fact]
    public void EmployeeServiceDeleteEmployeeShouldDeleteEmployee()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        
        var employee = testDataGenerator.GenerateEmployees().First();
        employeeService.Add(employee);
        
        //Act
        employeeService.Delete(employee);
        
        //Assert
        employeeStorage.Employees.Should().NotContain(employee);
    }
}