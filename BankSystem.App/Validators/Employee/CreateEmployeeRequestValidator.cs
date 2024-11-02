using BankSystem.App.Dto.Employee.Requests;
using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators.Employee;

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(request => request.Contract)
            .NotNullOrEmptyWithMessage(nameof(CreateEmployeeRequest.Contract));

        RuleFor(request => request.Salary)
            .GreaterThan(0).WithMessage(ExceptionMessages.TooLowValue(nameof(CreateEmployeeRequest.Salary)));

        RuleFor(request => request.FirstName)
            .NotNullOrEmptyWithMessage(nameof(CreateEmployeeRequest.FirstName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateEmployeeRequest.FirstName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateEmployeeRequest.FirstName)));

        RuleFor(request => request.LastName)
            .NotNullOrEmptyWithMessage(nameof(CreateEmployeeRequest.LastName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateEmployeeRequest.LastName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateEmployeeRequest.LastName)));

        RuleFor(request => request.BirthDay)
            .NotNullOrEmptyWithMessage(nameof(CreateEmployeeRequest.BirthDay))
            .LessThan(DateTime.Now.AddYears(-19)).WithMessage(ExceptionMessages.AgeRestriction(nameof(CreateEmployeeRequest.BirthDay)));

        RuleFor(request => request.PhoneNumber)
            .NotNullOrEmptyWithMessage(nameof(CreateEmployeeRequest.PhoneNumber))
            .Matches(@"^\+?[1-9]\d{0,2}[-\s]?(\(?\d{1,4}?\)?[-\s]?)?\d{1,4}[-\s]?\d{1,4}[-\s]?\d{1,9}$").WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateEmployeeRequest.PhoneNumber)))
            .WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateEmployeeRequest.PhoneNumber)));

        RuleFor(request => request.Email)
            .NotNullOrEmptyWithMessage(nameof(CreateEmployeeRequest.Email))
            .EmailAddress().WithMessage(ExceptionMessages.InvalidFormat(nameof(CreateEmployeeRequest.Email)));

        RuleFor(request => request.PassportNumber)
            .NotNullOrEmptyWithMessage(nameof(CreateEmployeeRequest.PassportNumber));
    }
}