namespace BankSystem.App.Dto.Client.Requests;

public record UpdateClientRequest(
    Guid Id,
    string BankAccountNumber,
    string FirstName,
    string LastName,
    DateTime BirthDay,
    string PhoneNumber,
    string Email,
    string PassportNumber
    );
