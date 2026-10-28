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
