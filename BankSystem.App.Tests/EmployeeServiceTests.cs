using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.App.Tests;

public class EmployeeServiceTests
{
    private readonly BankSystemContext _bankSystemContext;
    private readonly EmployeeStorage _employeeStorage;
    private readonly EmployeeService _employeeService;
    private readonly TestDataGenerator _testDataGenerator;

    public EmployeeServiceTests()
    {
        _bankSystemContext = new BankSystemContext();
        _employeeStorage = new EmployeeStorage(new BankSystemContext());
        _employeeService = new EmployeeService(_employeeStorage);
        _testDataGenerator = new TestDataGenerator();
    }

    [Fact]
    public async Task EmployeeServiceAddEmployeeShouldAddEmployee()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();
        var cancellationToken = new CancellationTokenSource().Token;
        
        //Act
        await _employeeService.AddAsync(employees.First(), cancellationToken);

        //Assert
        var existingEmployees = await _employeeStorage.GetAsync(1, employees.Count, null, cancellationToken);
        
        existingEmployees.Should().NotBeNull();
    }

    [Fact]
    public async Task EmployeeServiceGetPagedReturnsPagedEmployees()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();
        var cancellationToken = new CancellationTokenSource().Token;
        foreach (var item in employees)
        {
            await _employeeService.AddAsync(item, cancellationToken);
        }

        //Act
        var filteredEmployees = await _employeeService.GetPagedAsync(1, 10, null, cancellationToken);

        //Assert
        filteredEmployees.Should().NotBeNull();
    }

    [Fact]
    public async Task EmployeeServiceUpdateEmployeeShouldUpdateEmployee()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();
        var originalEmployee = employees.First();
        var updatedEmployee = employees.Last();
        var cancellationToken = new CancellationTokenSource().Token;

        await _employeeService.AddAsync(originalEmployee, cancellationToken);
        var employee = await _employeeService.GetByIdASync(originalEmployee.Id, cancellationToken);
        updatedEmployee.Id = employee.Id;

        //Act
        await _employeeService.UpdateAsync(updatedEmployee.Id,updatedEmployee, cancellationToken);

        var updatedEmployeeFromDb = _employeeService.GetByIdASync(originalEmployee.Id, cancellationToken);

        //Assert
        employee.Should().BeEquivalentTo(updatedEmployee);
    }

    [Fact]
    public async Task EmployeeServiceDeleteEmployeeShouldDeleteEmployee()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();
        var employee = employees.First();
        var cancellationToken = new CancellationTokenSource().Token;
        await _employeeService.AddAsync(employee, cancellationToken);

        //Act
        await _employeeService.DeleteAsync(employee.Id, cancellationToken);

        //Assert
        var existingEmployees = await _employeeStorage.GetAsync(1, employees.Count, null, cancellationToken);
        
        existingEmployees.Should().NotContain(employee);
    }
}