using BankSystem.App.Services;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.App.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public void EmployeeServiceAddEmployeeRangeShouldAddEmployees()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        var employees = testDataGenerator.GenerateEmployees();
        
        //Act
        employeeService.AddEmployeeRange(employees);

        //Assert
        employeeStorage.Employees.Should().NotBeNull();
    }
    
    [Fact]
    public void EmployeeServiceAddEmployeeShouldAddEmployee()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        
        var employees = testDataGenerator.GenerateEmployees();
        
        //Act
        employeeService.AddEmployee(employees.First());

        //Assert
        employeeStorage.Employees.Should().NotBeNull();
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
        
        employeeService.AddEmployee(originalEmployee);
        
        //Act
        employeeService.UpdateEmployee(originalEmployee, updatedEmployee);
        
        //Assert
        originalEmployee.Should().BeEquivalentTo(updatedEmployee);
    }

    [Fact]
    public void EmployeeServiceGetFilteredEmployeesReturnsFilteredEmployees()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        
        var employees = testDataGenerator.GenerateEmployees();
        var employee = employees.First();
        employeeService.AddEmployeeRange(employees);

        //Act
        var filteredEmployees = employeeService.GetFilteredEmployees(employee.FirstName, 
            employee.LastName, 
            employee.PhoneNumber, 
            employee.PassportNumber, 
            DateTime.MinValue, 
            DateTime.MaxValue);

        //Assert
        filteredEmployees.Should().NotBeNull();
    }
}