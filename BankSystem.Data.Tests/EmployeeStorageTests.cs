using BankSystem.App.Services;
using BankSystem.Data.EntityFramework;
using BankSystem.Data.Storages;
using FluentAssertions;
using Xunit;

namespace BankSystem.Data.Tests;

public class EmployeeStorageTests
{
    private readonly BankSystemContext _dbContext;
    private readonly EmployeeStorage _employeeStorage;
    private readonly TestDataGenerator _testDataGenerator;

    public EmployeeStorageTests()
    {
        _dbContext = new BankSystemContext();
        _employeeStorage = new EmployeeStorage(_dbContext);
        _testDataGenerator = new TestDataGenerator();
    }
    
}