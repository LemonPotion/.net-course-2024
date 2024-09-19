using System.Diagnostics.Contracts;
using Models.ValueObjects;

namespace Models;

/// <summary>
/// Модель сотрудника
/// </summary>
public class Employee : Person
{
    /// <summary>
    /// Контракт.
    /// </summary>
    public string? Contract { get; set; }
}