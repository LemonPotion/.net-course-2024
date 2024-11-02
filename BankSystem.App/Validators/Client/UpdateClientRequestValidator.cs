using BankSystem.App.Dto.Client.Requests;
using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators.Client;

public class UpdateClientRequestValidator : AbstractValidator<UpdateClientRequest>
{
    public UpdateClientRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.Id));
        
        RuleFor(request => request.BankAccountNumber)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.BankAccountNumber));

        RuleFor(request => request.FirstName)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.FirstName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateClientRequest.FirstName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateClientRequest.FirstName)));

        RuleFor(request => request.LastName)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.LastName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateClientRequest.LastName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateClientRequest.LastName)));

        RuleFor(request => request.BirthDay)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.BirthDay))
            .LessThan(DateTime.Now.AddYears(-19)).WithMessage(ExceptionMessages.AgeRestriction(nameof(UpdateClientRequest.BirthDay)));

        RuleFor(request => request.PhoneNumber)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.PhoneNumber))
            .Matches(@"^\+?[1-9]\d{0,2}[-\s]?(\(?\d{1,4}?\)?[-\s]?)?\d{1,4}[-\s]?\d{1,4}[-\s]?\d{1,9}$").WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateClientRequest.PhoneNumber)))
            .WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateClientRequest.PhoneNumber)));

        RuleFor(request => request.Email)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.Email))
            .EmailAddress().WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateClientRequest.Email)));

        RuleFor(request => request.PassportNumber)
            .NotNullOrEmptyWithMessage(nameof(UpdateClientRequest.PassportNumber));
    }
}