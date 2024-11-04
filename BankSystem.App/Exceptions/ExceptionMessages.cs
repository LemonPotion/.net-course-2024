namespace BankSystem.App.Exceptions;

public abstract class ExceptionMessages
{
    public static readonly Func<string, string> AgeRestriction = paramName => $"{paramName} age is under 18";
    
    public static readonly Func<string, string> PassportDataMissing = paramName => $"{paramName} is missing passport data";
    
    public static readonly Func<string, string> NullException = param => $"{param} is null";
    
    public static readonly Func<string, string> EmptyException = param => $"{param} is empty";
    
    public static readonly Func<string, string> InvalidFormat = param => $"{param} is invalid format";
    
    public static readonly Func<string, string> TooLowValue= param => $"{param} is too low value";
    
    public static readonly Func<string, string> TooHighValue= param => $"{param} is too high value";
}