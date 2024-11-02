using BankSystem.App.Dto.Employee.Responses;
using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators.Employee;

public class GetEmployeeByIdResponseValidator : AbstractValidator<GetEmployeeByIdResponse>
{
    public GetEmployeeByIdResponseValidator()
    {
        RuleFor(request => request.Contract)
            .NotNullOrEmptyWithMessage(nameof(GetEmployeeByIdResponse.Contract));

        RuleFor(request => request.Salary)
            .GreaterThan(0).WithMessage(ExceptionMessages.TooLowValue(nameof(GetEmployeeByIdResponse.Salary)));

        RuleFor(request => request.FirstName)
            .NotNullOrEmptyWithMessage(nameof(GetEmployeeByIdResponse.FirstName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(GetEmployeeByIdResponse.FirstName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(GetEmployeeByIdResponse.FirstName)));

        RuleFor(request => request.LastName)
            .NotNullOrEmptyWithMessage(nameof(GetEmployeeByIdResponse.LastName))
            .Matches("^[A-Za-zА-Яа-яЁё\\s]+$").WithMessage(ExceptionMessages.InvalidFormat(nameof(GetEmployeeByIdResponse.LastName)))
            .MaximumLength(50).WithMessage(ExceptionMessages.InvalidFormat(nameof(GetEmployeeByIdResponse.LastName)));

        RuleFor(request => request.BirthDay)
            .NotNullOrEmptyWithMessage(nameof(GetEmployeeByIdResponse.BirthDay))
            .LessThan(DateTime.Now.AddYears(-19)).WithMessage(ExceptionMessages.AgeRestriction(nameof(GetEmployeeByIdResponse.BirthDay)));

        RuleFor(request => request.PhoneNumber)
            .NotNullOrEmptyWithMessage(nameof(GetEmployeeByIdResponse.PhoneNumber))
            .Matches(@"^\+?[1-9]\d{0,2}[-\s]?(\(?\d{1,4}?\)?[-\s]?)?\d{1,4}[-\s]?\d{1,4}[-\s]?\d{1,9}$").WithMessage(ExceptionMessages.InvalidFormat(nameof(GetEmployeeByIdResponse.PhoneNumber)))
            .WithMessage(ExceptionMessages.InvalidFormat(nameof(GetEmployeeByIdResponse.PhoneNumber)));

        RuleFor(request => request.Email)
            .NotNullOrEmptyWithMessage(nameof(GetEmployeeByIdResponse.Email))
            .EmailAddress().WithMessage(ExceptionMessages.InvalidFormat(nameof(GetEmployeeByIdResponse.Email)));

        RuleFor(request => request.PassportNumber)
            .NotNullOrEmptyWithMessage(nameof(GetEmployeeByIdResponse.PassportNumber));
    }
}