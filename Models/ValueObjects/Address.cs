namespace Models.ValueObjects;
/// <summary>
/// Value object хранящий адрес
/// </summary>
public class Address : BaseValueObject
{
    /// <summary>
    /// Улица.
    /// </summary>
    public string Street { get; }

    /// <summary>
    /// Номер дома.
    /// </summary>
    public string HouseNumber { get; }

    /// <summary>
    /// Город.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Почтовый индекс.
    /// </summary>
    public string PostalCode { get; }

    /// <summary>
    /// Страна.
    /// </summary>
    public string Country { get; }
    
    public Address(string street, string houseNumber, string city, string postalCode, string country)
    {
        Street = street;
        HouseNumber = houseNumber;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }
}