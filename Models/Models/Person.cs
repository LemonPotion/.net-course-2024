using Models.ValueObjects;

namespace BankSystem.Domain.Models;
/// <summary>
/// Модель человека.
/// </summary>
public class Person : BaseModel
{
    /// <summary>
    /// Полное имя.
    /// </summary>
    public FullName FullName { get; set; }

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

    /// <summary>
    /// Адрес проживания.
    /// </summary>
    public Address Address { get; set; }
}