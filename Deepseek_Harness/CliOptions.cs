namespace PhoneBookViewer;

/// <summary>Разобранные параметры командной строки.</summary>
public sealed class CliOptions
{
    public string? FilePath { get; private init; }

    public SortField? SortBy { get; private init; }

    public bool ShowHelp { get; private init; }

    public bool ShowVersion { get; private init; }

    public enum SortField
    {
        Name,
        Category,
    }

    public const string HelpText = """
        Показ всех записей телефонной книги.

        Использование:
          PhoneBookViewer [параметры]

        Параметры:
          -f, --file <путь>   JSON-файл с телефонной книгой
                              (по умолчанию data/phonebook.json)
          -s, --sort <поле>   сортировка перед выводом: name | category
          -h, --help          показать эту справку
          -?, -v, --version   показать версию

        Примеры:
          PhoneBookViewer
          PhoneBookViewer --file C:\data\contacts.json
          PhoneBookViewer --sort name
        """;

    public static bool TryParse(string[] args, out CliOptions options, out string? error)
    {
        string? filePath = null;
        SortField? sortBy = null;
        bool showHelp = false;
        bool showVersion = false;

        error = null;
        options = new CliOptions();

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];

            switch (arg)
            {
                case "-h":
                case "--help":
                case "-?":
                    showHelp = true;
                    break;

                case "-v":
                case "--version":
                    showVersion = true;
                    break;

                case "-f":
                case "--file":
                    if (!TryTakeValue(args, ref i, out string? value))
                    {
                        error = $"для параметра {arg} не указан путь к файлу";
                        return false;
                    }

                    filePath = value;
                    break;

                case "-s":
                case "--sort":
                    if (!TryTakeValue(args, ref i, out string? sortValue))
                    {
                        error = $"для параметра {arg} не указано поле сортировки";
                        return false;
                    }

                    if (!TryParseSort(sortValue!, out SortField field))
                    {
                        error = $"неизвестное поле сортировки «{sortValue}» (ожидается name или category)";
                        return false;
                    }

                    sortBy = field;
                    break;

                default:
                    error = $"неизвестный параметр «{arg}»";
                    return false;
            }
        }

        options = new CliOptions
        {
            FilePath = filePath,
            SortBy = sortBy,
            ShowHelp = showHelp,
            ShowVersion = showVersion,
        };
        return true;
    }

    private static bool TryTakeValue(string[] args, ref int index, out string? value)
    {
        value = null;
        if (index + 1 >= args.Length)
        {
            return false;
        }

        value = args[++index];
        return true;
    }

    private static bool TryParseSort(string value, out SortField field)
    {
        switch (value.Trim().ToLowerInvariant())
        {
            case "name":
            case "имя":
                field = SortField.Name;
                return true;
            case "category":
            case "категория":
                field = SortField.Category;
                return true;
            default:
                field = default;
                return false;
        }
    }
}
