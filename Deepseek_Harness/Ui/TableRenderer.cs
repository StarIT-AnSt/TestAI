using System.Text;
using PhoneBookViewer.Models;

namespace PhoneBookViewer.Ui;

/// <summary>
/// Рисует телефонную книгу в виде текстовой таблицы.
/// </summary>
public static class TableRenderer
{
    private const int MaxNoteWidth = 34;

    /// <summary>Формирует таблицу со всеми записями и итоговой строкой.</summary>
    public static IReadOnlyList<string> Render(IReadOnlyList<PhoneBookEntry> entries, string filePath)
    {
        var lines = new List<string>
        {
            "═══ Телефонная книга ═══",
            $"Файл данных: {filePath}",
            string.Empty,
        };

        if (entries.Count == 0)
        {
            lines.Add("Телефонная книга пуста.");
            return lines;
        }

        string[] headers = ["№", "Имя", "Телефон", "Категория", "Примечание"];

        // Сначала обрезаем слишком длинные примечания, чтобы таблица не «разъезжалась».
        string[] notes = entries
            .Select(e => Truncate(Normalize(e.Note), MaxNoteWidth))
            .ToArray();

        var columns = new List<(bool RightAlign, int Width)>
        {
            (true, Math.Max(headers[0].Length, entries.Count.ToString().Length)),
            (false, headers[1].Length),
            (false, headers[2].Length),
            (false, headers[3].Length),
            (false, headers[4].Length),
        };

        for (int i = 0; i < entries.Count; i++)
        {
            columns[1] = (false, Math.Max(columns[1].Width, entries[i].Name.Length));
            columns[2] = (false, Math.Max(columns[2].Width, entries[i].Phone.Length));
            columns[3] = (false, Math.Max(columns[3].Width, entries[i].Category.Length));
            columns[4] = (false, Math.Max(columns[4].Width, notes[i].Length));
        }

        lines.Add(BuildRow(headers, columns));
        lines.Add(BuildSeparator(columns));

        for (int i = 0; i < entries.Count; i++)
        {
            string[] cells =
            [
                (i + 1).ToString(),
                entries[i].Name,
                entries[i].Phone,
                entries[i].Category,
                notes[i],
            ];
            lines.Add(BuildRow(cells, columns));
        }

        lines.Add(BuildSeparator(columns));
        lines.Add($"Всего записей: {entries.Count}");
        lines.Add($"По категориям: {BuildCategorySummary(entries)}");

        return lines;
    }

    private static string BuildRow(IReadOnlyList<string> cells, List<(bool RightAlign, int Width)> columns)
    {
        var builder = new StringBuilder();

        for (int i = 0; i < cells.Count; i++)
        {
            (bool rightAlign, int width) = columns[i];
            string cell = cells[i];
            string padded = rightAlign
                ? cell.PadLeft(width)
                : cell.PadRight(width);

            builder.Append(i == 0 ? "  " : " │ ");
            builder.Append(padded);
        }

        return builder.ToString().TrimEnd();
    }

    private static string BuildSeparator(List<(bool RightAlign, int Width)> columns)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < columns.Count; i++)
        {
            builder.Append(i == 0 ? "──" : "─┼─");
            builder.Append(new string('─', columns[i].Width));
        }

        return builder.ToString();
    }

    private static string BuildCategorySummary(IReadOnlyList<PhoneBookEntry> entries)
    {
        return string.Join(
            ", ",
            entries
                .GroupBy(e => e.Category)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key, StringComparer.CurrentCulture)
                .Select(g => $"{g.Key} — {g.Count()}"));
    }

    private static string Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? "—" : value.Trim();

    private static string Truncate(string value, int maxLength)
    {
        if (value.Length <= maxLength)
        {
            return value;
        }

        return string.Concat(value.AsSpan(0, maxLength - 1), "…");
    }
}
