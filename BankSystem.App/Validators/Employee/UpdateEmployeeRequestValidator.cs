using BankSystem.App.Dto.Employee.Requests;
using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators.Employee;

public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.Id));
        
        RuleFor(request => request.Contract)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.Contract));

        RuleFor(request => request.Salary)
            .GreaterThan(0).WithMessage(ExceptionMessages.TooLowValue(nameof(UpdateEmployeeRequest.Salary)));

        RuleFor(request => request.FirstName)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.FirstName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateEmployeeRequest.FirstName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateEmployeeRequest.FirstName)));

        RuleFor(request => request.LastName)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.LastName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateEmployeeRequest.LastName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateEmployeeRequest.LastName)));

        RuleFor(request => request.BirthDay)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.BirthDay))
            .LessThan(DateTime.Now.AddYears(-19)).WithMessage(ExceptionMessages.AgeRestriction(nameof(UpdateEmployeeRequest.BirthDay)));

        RuleFor(request => request.PhoneNumber)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.PhoneNumber))
            .Matches(@"^\+?[1-9]\d{0,2}[-\s]?(\(?\d{1,4}?\)?[-\s]?)?\d{1,4}[-\s]?\d{1,4}[-\s]?\d{1,9}$").WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateEmployeeRequest.PhoneNumber)))
            .WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateEmployeeRequest.PhoneNumber)));

        RuleFor(request => request.Email)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.Email))
            .EmailAddress().WithMessage(ExceptionMessages.InvalidFormat(nameof(UpdateEmployeeRequest.Email)));

        RuleFor(request => request.PassportNumber)
            .NotNullOrEmptyWithMessage(nameof(UpdateEmployeeRequest.PassportNumber));
    }
}