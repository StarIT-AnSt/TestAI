namespace PhoneBookViewer.Models;

/// <summary>
/// Одна запись телефонной книги.
/// </summary>
public sealed record PhoneBookEntry
{
    /// <summary>Имя и фамилия абонента.</summary>
    public required string Name { get; init; }

    /// <summary>Номер телефона в произвольном формате.</summary>
    public required string Phone { get; init; }

    /// <summary>Категория (семья, друзья, работа и т. п.).</summary>
    public string Category { get; init; } = "Без категории";

    /// <summary>Необязательное примечание.</summary>
    public string? Note { get; init; }
}
