# Программа для показа всех записей телефонной книги на C#

## Вариант 1: Простой консольный вариант с классом

```csharp
using System;
using System.Collections.Generic;

// Модель записи телефонной книги
public class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"{Name,-25} {Phone,-15} {Email}";
    }
}

public class PhoneBook
{
    private readonly List<Contact> _contacts = new();

    public void Add(string name, string phone, string email)
    {
        _contacts.Add(new Contact { Name = name, Phone = phone, Email = email });
    }

    // Показать все записи
    public void ShowAll()
    {
        if (_contacts.Count == 0)
        {
            Console.WriteLine("Телефонная книга пуста.");
            return;
        }

        Console.WriteLine($"{"Имя",-25} {"Телефон",-15} {"Email"}");
        Console.WriteLine(new string('-', 60));

        foreach (var contact in _contacts)
        {
            Console.WriteLine(contact);
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"Всего записей: {_contacts.Count}");
    }
}

class Program
{
    static void Main()
    {
        var phoneBook = new PhoneBook();

        // Заполняем тестовыми данными
        phoneBook.Add("Иван Иванов",      "+7-900-111-22-33", "ivan@mail.ru");
        phoneBook.Add("Мария Петрова",    "+7-900-222-33-44", "maria@mail.ru");
        phoneBook.Add("Сергей Сидоров",   "+7-900-333-44-55", "sergey@mail.ru");
        phoneBook.Add("Анна Кузнецова",   "+7-900-444-55-66", "anna@mail.ru");

        // Показываем все записи
        phoneBook.ShowAll();
    }
}
```

## Вариант 2: Чтение из файла (JSON)

Если записи хранятся в файле `phonebook.json`, используйте `System.Text.Json`:

```csharp
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
```

Пример `phonebook.json`:

```json
[
  { "Name": "Иван Иванов",    "Phone": "+7-900-111-22-33", "Email": "ivan@mail.ru" },
  { "Name": "Мария Петрова",  "Phone": "+7-900-222-33-44", "Email": "maria@mail.ru" }
]
```

## Пример вывода

```
Имя                       Телефон         Email
------------------------------------------------------------
Иван Иванов               +7-900-111-22-33 ivan@mail.ru
Мария Петрова             +7-900-222-33-44 maria@mail.ru
Сергей Сидоров            +7-900-333-44-55 sergey@mail.ru
Анна Кузнецова            +7-900-444-55-66 anna@mail.ru
------------------------------------------------------------
Всего записей: 4
```

## Что можно добавить дальше

- **Меню** с выбором действий (просмотр, добавление, поиск, удаление);
- **Поиск** по имени или телефону (`Where` + `Contains`);
- **Сортировку** по имени (`OrderBy(c => c.Name)`);
- **Сохранение** изменений обратно в файл (`JsonSerializer.Serialize` + `File.WriteAllText`);
- **Валидацию** телефона и email.

Если нужен GUI-вариант (WinForms / WPF / MAUI) или сохранение в SQLite — скажите, покажу отдельный пример.

https://chat.deepseek.com/share/af7hawlc2j0uqgzl4x
