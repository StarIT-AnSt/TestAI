using System.Security;
using System.Text;
using System.Text.Json;
using PhoneBookViewer.Models;
using PhoneBookViewer.Services;
using PhoneBookViewer.Ui;

namespace PhoneBookViewer;

internal static class Program
{
    private static int Main(string[] args)
    {
        ConfigureConsole();

        if (!CliOptions.TryParse(args, out CliOptions options, out string? parseError))
        {
            Console.Error.WriteLine($"Ошибка: {parseError}");
            Console.Error.WriteLine("Справка: PhoneBookViewer --help");
            return 2;
        }

        if (options.ShowHelp)
        {
            Console.WriteLine(CliOptions.HelpText);
            return 0;
        }

        if (options.ShowVersion)
        {
            Console.WriteLine($"PhoneBookViewer 1.0.0 ({Environment.Version})");
            return 0;
        }

        try
        {
            var repository = new PhoneBookRepository(ResolveDataFile(options.FilePath));
            IReadOnlyList<PhoneBookEntry> entries = repository.Load();
            IEnumerable<PhoneBookEntry> ordered = ApplySort(entries, options.SortBy);
            IReadOnlyList<string> lines = TableRenderer.Render(ordered.ToList(), repository.FilePath);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            return 0;
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Файл телефонной книги повреждён: {ex.Message}");
            return 1;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"Не удалось прочитать телефонную книгу: {ex.Message}");
            return 1;
        }
    }

    private static void ConfigureConsole()
    {
        try
        {
            // Чтобы кириллица и псевдографика выводились корректно в консоли Windows.
            Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            Console.InputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        }
        catch (IOException)
        {
            // Консоль может не поддерживать смену кодировки (например, при перенаправлении вывода).
        }
        catch (SecurityException)
        {
            // Ничего страшного: продолжаем с кодировкой по умолчанию.
        }
    }

    /// <summary>
    /// Ищет файл данных: сначала явно указанный путь, затем папку приложения,
    /// затем текущую папку, затем — на уровень выше (запуск через dotnet run).
    /// </summary>
    private static string ResolveDataFile(string? explicitPath)
    {
        if (!string.IsNullOrWhiteSpace(explicitPath))
        {
            return explicitPath;
        }

        string fileName = Path.Combine("data", "phonebook.json");
        string baseDir = AppContext.BaseDirectory;
        string currentDir = Directory.GetCurrentDirectory();

        string[] candidates =
        [
            Path.Combine(baseDir, fileName),
            Path.Combine(currentDir, fileName),
            Path.GetFullPath(Path.Combine(currentDir, "..", fileName)),
        ];

        foreach (string candidate in candidates)
        {
            if (File.Exists(candidate))
            {
                return candidate;
            }
        }

        return candidates[0];
    }

    private static IEnumerable<PhoneBookEntry> ApplySort(IEnumerable<PhoneBookEntry> entries, CliOptions.SortField? sortBy)
    {
        return sortBy switch
        {
            CliOptions.SortField.Name => entries
                .OrderBy(e => e.Name, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(e => e.Phone, StringComparer.Ordinal),
            CliOptions.SortField.Category => entries
                .OrderBy(e => e.Category, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(e => e.Name, StringComparer.CurrentCultureIgnoreCase),
            _ => entries,
        };
    }
}
