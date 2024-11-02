using BankSystem.App.Dto.Client.Requests;
using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators.Client;

public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator()
    {
        RuleFor(request => request.BankAccountNumber)
            .NotNullOrEmptyWithMessage(nameof(CreateClientRequest.BankAccountNumber));

        RuleFor(request => request.FirstName)
            .NotNullOrEmptyWithMessage(nameof(CreateClientRequest.FirstName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateClientRequest.FirstName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateClientRequest.FirstName)));

        RuleFor(request => request.LastName)
            .NotNullOrEmptyWithMessage(nameof(CreateClientRequest.LastName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateClientRequest.LastName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateClientRequest.LastName)));

        RuleFor(request => request.BirthDay)
            .NotNullOrEmptyWithMessage(nameof(CreateClientRequest.BirthDay))
            .LessThan(DateTime.Now.AddYears(-19)).WithMessage(ExceptionMessages.AgeRestriction(nameof(CreateClientRequest.BirthDay)));

        RuleFor(request => request.PhoneNumber)
            .NotNullOrEmptyWithMessage(nameof(CreateClientRequest.PhoneNumber))
            .Matches(@"^\+?[1-9]\d{0,2}[-\s]?(\(?\d{1,4}?\)?[-\s]?)?\d{1,4}[-\s]?\d{1,4}[-\s]?\d{1,9}$").WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateClientRequest.PhoneNumber)))
            .WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateClientRequest.PhoneNumber)));

        RuleFor(request => request.Email)
            .NotNullOrEmptyWithMessage(nameof(CreateClientRequest.Email))
            .EmailAddress().WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateClientRequest.Email)));

        RuleFor(request => request.PassportNumber)
            .NotNullOrEmptyWithMessage(nameof(CreateClientRequest.PassportNumber));
    }
}