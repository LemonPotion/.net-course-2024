using BankSystem.App.Exceptions;
using FluentValidation;

namespace BankSystem.App.Validators;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> NotNullOrEmptyWithMessage<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder, 
        string paramName)
    {
        return ruleBuilder
            .NotNull().WithMessage(ExceptionMessages.NullException(paramName))
            .NotEmpty().WithMessage(ExceptionMessages.EmptyException(paramName));
    }
}