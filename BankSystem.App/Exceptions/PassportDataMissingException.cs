namespace BankSystem.App.Exceptions;

public class PassportDataMissingException : Exception
{
    public PassportDataMissingException(string paramName) : base(ExceptionMessages.PassportDataMissing(paramName))
    {
        
    }
}