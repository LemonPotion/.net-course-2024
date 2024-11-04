using BankSystem.App.Dto.Client.Responses;
using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators.Client;

public class GetClientByIdResponseValidator : AbstractValidator<GetClientByIdResponse>
{
    public GetClientByIdResponseValidator()
    {
        RuleFor(request => request.Id)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.Id));
        
        RuleFor(request => request.BankAccountNumber)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.BankAccountNumber));

        RuleFor(request => request.FirstName)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.FirstName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(GetClientByIdResponse.FirstName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(GetClientByIdResponse.FirstName)));

        RuleFor(request => request.LastName)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.LastName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(GetClientByIdResponse.FirstName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(GetClientByIdResponse.LastName)));

        RuleFor(request => request.BirthDay)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.BirthDay))
            .LessThan(DateTime.Now.AddYears(-19)).WithMessage(ExceptionMessages.AgeRestriction(nameof(GetClientByIdResponse.BirthDay)));

        RuleFor(request => request.PhoneNumber)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.PhoneNumber))
            .Matches(@"^\+?[1-9]\d{0,2}[-\s]?(\(?\d{1,4}?\)?[-\s]?)?\d{1,4}[-\s]?\d{1,4}[-\s]?\d{1,9}$").WithMessage(ExceptionMessages.InvalidFormat(nameof(GetClientByIdResponse.PhoneNumber)))
            .WithMessage(ExceptionMessages.InvalidFormat(nameof(GetClientByIdResponse.PhoneNumber)));

        RuleFor(request => request.Email)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.Email))
            .EmailAddress().WithMessage(ExceptionMessages.InvalidFormat(nameof(GetClientByIdResponse.Email)));

        RuleFor(request => request.PassportNumber)
            .NotNullOrEmptyWithMessage(nameof(GetClientByIdResponse.PassportNumber));
    }
}