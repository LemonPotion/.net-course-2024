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
    public void EmployeeServiceAddEmployeeShouldAddEmployee()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();

        //Act
        _employeeService.Add(employees.First());

        //Assert
        _employeeStorage.Get(1, employees.Count, null).Should().NotBeNull();
    }

    [Fact]
    public void EmployeeServiceGetPagedReturnsPagedEmployees()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();
        foreach (var item in employees)
        {
            _employeeService.Add(item);
        }

        //Act
        var filteredEmployees = _employeeService.GetPaged(1, 10, null);

        //Assert
        filteredEmployees.Should().NotBeNull();
    }

    [Fact]
    public void EmployeeServiceUpdateEmployeeShouldUpdateEmployee()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();
        var originalEmployee = employees.First();
        var updatedEmployee = employees.Last();

        _employeeService.Add(originalEmployee);
        var employee = _employeeService.GetById(originalEmployee.Id);
        updatedEmployee.Id = employee.Id;

        //Act
        _employeeService.Update(updatedEmployee);

        var updatedEmployeeFromDb = _employeeService.GetById(originalEmployee.Id);

        //Assert
        employee.Should().BeEquivalentTo(updatedEmployee);
    }

    [Fact]
    public void EmployeeServiceDeleteEmployeeShouldDeleteEmployee()
    {
        //Arrange
        var employees = _testDataGenerator.GenerateEmployees();
        var employee = employees.First();
        _employeeService.Add(employee);

        //Act
        _employeeService.Delete(employee.Id);

        //Assert
        _employeeStorage.Get(1, employees.Count, null).Should().NotContain(employee);
    }
}