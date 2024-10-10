namespace BankSystem.App.Exceptions;

public class AgeRestrictionException : Exception
{
    public AgeRestrictionException(string paramName) : base(ExceptionMessages.AgeRestriction(paramName))
    {
    }
}