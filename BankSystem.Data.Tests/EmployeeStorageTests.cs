using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class EmployeeStorageTests
{
    private readonly BankSystemContext _bankSystemContext;
    private readonly EmployeeStorage _employeeStorage;
    private readonly TestDataGenerator _testDataGenerator;

    public EmployeeStorageTests()
    {
        _bankSystemContext = new BankSystemContext();
        _employeeStorage = new EmployeeStorage(_bankSystemContext);
        _testDataGenerator = new TestDataGenerator();
    }

    [Fact]
    public async Task EmployeeStorageAddEmployeeShouldAddEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        var cancellationToken = new CancellationTokenSource().Token;
        
        //Act
        await _employeeStorage.AddAsync(employee, cancellationToken);

        //Assert
        var existingEmployee = await _employeeStorage.GetByIdAsync(employee.Id, cancellationToken); 
        
        existingEmployee.Should().BeEquivalentTo(employee);
    }

    [Fact]
    public async Task EmployeeStorageGetEmployeeByIdPagedShouldReturnEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        var cancellationToken = new CancellationTokenSource().Token; 
        await _employeeStorage.AddAsync(employee, cancellationToken);

        //Act
        var result = await _employeeStorage.GetByIdAsync(employee.Id, cancellationToken);

        //Assert
        var existingEmployee = await _employeeStorage.GetByIdAsync(employee.Id, cancellationToken); 
        
        existingEmployee.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task EmployeeStorageGetEmployeesPagedShouldReturnEmployees()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        var cancellationToken = new CancellationTokenSource().Token;
        await _employeeStorage.AddAsync(employee, cancellationToken);

        //Act
        var result = await _employeeStorage.GetAsync(1, _bankSystemContext.Employees.Count(), null, cancellationToken);

        //Assert
        var existingEmployees = await _employeeStorage.GetAsync(1, _bankSystemContext.Employees.Count(), null, cancellationToken); 
        
        existingEmployees.Should().Contain(result);
    }

    [Fact]
    public async Task EmployeeStorageUpdateEmployeeShouldUpdateEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        var updatedEmployee = _testDataGenerator.GenerateEmployees(1).First();
        var cancellationToken = new CancellationTokenSource().Token;
        await _employeeStorage.AddAsync(employee, cancellationToken);
        updatedEmployee.Id = employee.Id;

        //Act
        await _employeeStorage.UpdateAsync(updatedEmployee.Id,  updatedEmployee, cancellationToken);

        //Assert
        var existingEmployee = await _employeeStorage.GetByIdAsync(employee.Id, cancellationToken); 
        
        existingEmployee.Should().Be(updatedEmployee);
    }

    [Fact]
    public async Task EmployeeStorageDeleteEmployeeShouldDeleteEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        var cancellationToken = new CancellationTokenSource().Token;
        await _employeeStorage.AddAsync(employee, cancellationToken);


        //Act
        await _employeeStorage.DeleteAsync(employee.Id, cancellationToken);

        //Assert
        var existingEmployee = await _employeeStorage.GetByIdAsync(employee.Id, cancellationToken); 
        
        existingEmployee.Should().BeNull();
    }
}