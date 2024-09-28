﻿using BankSystem.App.Services;
using BankSystem.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BankSystem.Tests;

public class EquivalenceTests
{
    [Fact]
    public void GetHashCodeNecessityPositiveTest()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientAccountDictionary = testDataGenerator.GenerateClientAccount();
        var (key, value) = clientAccountDictionary.ElementAtOrDefault(15);
        var client = new Client()
        {
            BankAccountNumber = key.BankAccountNumber,
            BirthDay = key.BirthDay,
            Email = key.Email,
            Firstname = key.Firstname,
            LastName = key.LastName,
            PhoneNumber = key.PhoneNumber
        };
        
        //Act
        var account = clientAccountDictionary[client];

        //Assert
        account.Should().BeEquivalentTo(value);
    }
    
    [Fact]
    public void ClientAccountsContainsEquivalentClientReturnsExpectedAccount()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var clientAccounts = testDataGenerator.GenerateClientAccounts();
        var (key, value) = clientAccounts.ElementAtOrDefault(15);
        var client = new Client()
        {
            BankAccountNumber = key.BankAccountNumber,
            BirthDay = key.BirthDay,
            Email = key.Email,
            Firstname = key.Firstname,
            LastName = key.LastName,
            PhoneNumber = key.PhoneNumber
        };
        
        //Act
        var account = clientAccounts[client];

        //Assert
        account.Should().BeEquivalentTo(value);
    }

    [Fact]
    public void EmployeeListContainsEquivalentEmployeeReturnsTrue()
    {
        //Arrange
        var testDataGenerator = new TestDataGenerator();
        var employees = testDataGenerator.GenerateEmployees();
        var employee = employees[15];
        var newEmployee = new Employee()
        {
            BirthDay = employee.BirthDay,
            Contract = employee.Contract,
            Email = employee.Email,
            Firstname = employee.Firstname,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Salary = employee.Salary
        };
        
        //Act
        var containsEmployee = employees.Contains(newEmployee);

        //Assert
        containsEmployee.Should().Be(true);
    }
}