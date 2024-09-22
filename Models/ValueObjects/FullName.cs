namespace Models.ValueObjects;

/// <summary>
/// Value object хранящий полное ФИО человека.
/// </summary>
public class FullName : BaseValueObject
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; } 
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string? MiddleName { get; set; }
    
    public FullName(string firstName, string lastName, string? middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
}