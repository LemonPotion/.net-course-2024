namespace BankSystem.App.Dto.Client.Requests;

public record CreateClientRequest(
    string BankAccountNumber,
    string FirstName,
    string LastName,
    DateTime BirthDay,
    string PhoneNumber,
    string Email,
    string PassportNumber
    );