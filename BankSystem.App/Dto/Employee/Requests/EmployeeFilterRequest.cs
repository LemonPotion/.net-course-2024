namespace BankSystem.App.Dto.Employee.Requests;

public record EmployeeFilterRequest(
    string FirstName,
    string LastName,
    DateTime BirthDay,
    string PhoneNumber,
    string Email,
    string PassportNumber,
    string Contract,
    decimal Salary
    );