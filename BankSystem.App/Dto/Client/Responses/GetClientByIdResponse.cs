namespace BankSystem.App.Dto.Client.Responses;

public record GetClientByIdResponse(
    Guid Id,
    string BankAccountNumber,
    string FirstName,
    string LastName,
    DateTime BirthDay,
    string PhoneNumber,
    string Email,
    string PassportNumber
    );