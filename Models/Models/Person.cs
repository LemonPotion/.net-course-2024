
namespace BankSystem.Domain.Models;
/// <summary>
/// Модель человека.
/// </summary>
public class Person
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Firstname { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime BirthDay { get; set; }

    /// <summary>
    /// Возраст.
    /// </summary>
    public int Age => DateTime.Now.Year - BirthDay.Year;

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }
    
}