namespace Models;

/// <summary>
/// Базовый абстрактный класс модели.
/// </summary>
public abstract class BaseModel
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        else if (obj is not BaseModel entity)
            return false;
        else if (entity.Id != Id)
            return false;
        return true;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    /// <summary>
    /// Переопределение метода для получения всех значений в виде string.
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        var props = GetType().GetProperties();
        var values = props.Select(prop => $"{prop.Name}: {prop.GetValue(this) ?? "null"}");
        return string.Join(" ", values);
    }
}