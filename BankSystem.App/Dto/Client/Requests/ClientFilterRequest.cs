namespace BankSystem.App.Dto.Client.Requests;

public record ClientFilterRequest(
    string FirstName,
    string LastName,
    DateTime BirthDay,
    string PhoneNumber,
    string Email,
    string PassportNumber,
    string BankAccountNumber
    );