using BankSystem.App.Dto;
using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators;

public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ExceptionMessages.TooLowValue(nameof(PaginationRequest.PageNumber)));

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ExceptionMessages.TooLowValue(nameof(PaginationRequest.PageSize)))
            .LessThanOrEqualTo(100)
            .WithMessage(ExceptionMessages.TooHighValue(nameof(PaginationRequest.PageSize)));
    }
}