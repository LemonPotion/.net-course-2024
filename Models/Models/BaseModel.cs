namespace BankSystem.Domain.Models;

/// <summary>
/// Базовый абстрактный класс модели.
/// </summary>
public abstract class BaseModel
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
}