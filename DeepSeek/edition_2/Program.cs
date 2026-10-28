using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

class Program
{
    static void Main()
    {
        string filePath = "phonebook.json";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл {filePath} не найден.");
            return;
        }

        string json = File.ReadAllText(filePath);
        var contacts = JsonSerializer.Deserialize<List<Contact>>(json);

        if (contacts == null || contacts.Count == 0)
        {
            Console.WriteLine("Телефонная книга пуста.");
            return;
        }

        Console.WriteLine($"{"Имя",-25} {"Телефон",-15} {"Email"}");
        Console.WriteLine(new string('-', 60));

        foreach (var c in contacts)
            Console.WriteLine($"{c.Name,-25} {c.Phone,-15} {c.Email}");

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"Всего записей: {contacts.Count}");
    }
}
