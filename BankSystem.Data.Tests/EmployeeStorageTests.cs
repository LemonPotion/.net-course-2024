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
    public void EmployeeStorageAddEmployeeShouldAddEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();

        //Act
        _employeeStorage.Add(employee);

        //Assert
        _employeeStorage.GetById(employee.Id).Should().BeEquivalentTo(employee);
    }

    [Fact]
    public void EmployeeStorageGetEmployeeByIdPagedShouldReturnEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        _employeeStorage.Add(employee);

        //Act
        var result = _employeeStorage.GetById(employee.Id);

        //Assert
        _employeeStorage.GetById(employee.Id).Should().BeEquivalentTo(result);
    }

    [Fact]
    public void EmployeeStorageGetEmployeesPagedShouldReturnEmployees()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        _employeeStorage.Add(employee);

        //Act
        var result = _employeeStorage.Get(1, _bankSystemContext.Employees.Count(), null);

        //Assert
        _employeeStorage.Get(1, _bankSystemContext.Employees.Count(), null).Should().Contain(result);
    }

    [Fact]
    public void EmployeeStorageUpdateEmployeeShouldUpdateEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        var updatedEmployee = _testDataGenerator.GenerateEmployees(1).First();
        _employeeStorage.Add(employee);
        updatedEmployee.Id = employee.Id;

        //Act
        _employeeStorage.Update(updatedEmployee.Id,  updatedEmployee);

        //Assert
        _employeeStorage.GetById(employee.Id).Should().Be(updatedEmployee);
    }

    [Fact]
    public void EmployeeStorageDeleteEmployeeShouldDeleteEmployee()
    {
        //Arrange
        var employee = _testDataGenerator.GenerateEmployees(1).First();
        _employeeStorage.Add(employee);


        //Act
        _employeeStorage.Delete(employee.Id);

        //Assert
        _employeeStorage.GetById(employee.Id).Should().BeNull();
    }
}