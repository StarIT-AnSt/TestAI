using System.Text.Encodings.Web;
using System.Text.Json;
using PhoneBookViewer.Models;

namespace PhoneBookViewer.Services;

/// <summary>
/// Загружает и сохраняет телефонную книгу в JSON-файле.
/// Если файла нет, создаёт его с образцом данных.
/// </summary>
public sealed class PhoneBookRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        // Кириллица в JSON остаётся читаемой, а не \uXXXX.
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    public PhoneBookRepository(string filePath)
    {
        FilePath = Path.GetFullPath(filePath);
    }

    /// <summary>Полный путь к файлу данных.</summary>
    public string FilePath { get; }

    /// <summary>
    /// Читает все записи. Если файла ещё нет, создаёт его с образцом данных.
    /// </summary>
    public IReadOnlyList<PhoneBookEntry> Load()
    {
        if (!File.Exists(FilePath))
        {
            CreateSampleFile();
        }

        string json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        List<PhoneBookEntry>? entries = JsonSerializer.Deserialize<List<PhoneBookEntry>>(json, JsonOptions);

        return entries ?? [];
    }

    /// <summary>Образец телефонной книги.</summary>
    public static IReadOnlyList<PhoneBookEntry> SampleEntries =>
    [
        new() { Name = "Анна Иванова", Phone = "+7 916 123-45-67", Category = "Семья", Note = "Домашний номер" },
        new() { Name = "Пётр Иванов", Phone = "+7 903 555-10-20", Category = "Семья", Note = "Мобильный" },
        new() { Name = "Ольга Смирнова", Phone = "+7 495 123-45-67", Category = "Работа", Note = "Бухгалтерия" },
        new() { Name = "Сергей Кузнецов", Phone = "+7 921 777-88-99", Category = "Друзья", Note = "Рыбалка, пиво" },
        new() { Name = "Мария Попова", Phone = "+7 911 222-33-44", Category = "Друзья" },
        new() { Name = "Алексей Соколов", Phone = "+7 985 100-20-30", Category = "Работа", Note = "Отдел закупок" },
        new() { Name = "Екатерина Волкова", Phone = "+7 926 444-55-66", Category = "Семья", Note = "Двоюродная сестра" },
        new() { Name = "Дмитрий Лебедев", Phone = "+7 999 888-77-66", Category = "Сервис", Note = "Автосервис на Ленина" },
    ];

    private void CreateSampleFile()
    {
        string? directory = Path.GetDirectoryName(FilePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(SampleEntries, JsonOptions);
        File.WriteAllText(FilePath, json);
        Console.WriteLine($"Файл не найден — создан образец: {FilePath}");
        Console.WriteLine();
    }
}
