namespace BankSystem.App.Dto.Employee.Responses;

public record GetEmployeeByIdResponse(
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