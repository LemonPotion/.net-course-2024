using System.Text.Json;

namespace Models.ValueObjects;
/// <summary>
/// Базовый класс для value object.
/// </summary>
public class BaseValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj is not BaseValueObject valueObject)
            return false;
        var serializedValueObject = Serialize(valueObject);
        var serializedThis = Serialize(this);
            
        return string.CompareOrdinal(serializedValueObject, serializedThis) == 0;
    }
    
    public override int GetHashCode()
    {
        return Serialize(this).GetHashCode();
    }

    /// <summary>
    /// Метод для сериализации обьектов BaseValueObject.
    /// </summary>
    /// <param name="valueObject">value object для сериализации.</param>
    /// <returns>Сериализованный обьект.</returns>
    public string Serialize(BaseValueObject valueObject)
    {
        var serializedValueObject = JsonSerializer.Serialize(valueObject);
        return serializedValueObject;
    }
}