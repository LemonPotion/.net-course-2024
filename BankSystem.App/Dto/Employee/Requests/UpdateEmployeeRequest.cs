﻿namespace BankSystem.App.Dto.Employee.Requests;

public record UpdateEmployeeRequest(
    Guid Id,
    string Contract,
    decimal Salary,
    string FirstName,
    string LastName,
    DateTime BirthDay,
    string PhoneNumber,
    string Email,
    string PassportNumber
    );