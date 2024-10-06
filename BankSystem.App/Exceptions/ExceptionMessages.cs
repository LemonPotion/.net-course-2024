namespace BankSystem.App.Exceptions;

public abstract class ExceptionMessages
{
    public static readonly Func<string, string> AgeRestriction = paramName => $"{paramName} age is under 18";
    
    public static readonly Func<string, string> PassportDataMissing = paramName => $"{paramName} is missing passport data";
}