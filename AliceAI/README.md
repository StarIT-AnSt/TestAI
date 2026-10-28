# Телефонная книга — консольный пример на C#

Простой пример программы на C#, которая выводит все записи телефонной книги. Для наглядности используется список объектов — в реальном проекте данные могут храниться в базе, файле или API.

## Вариант с консольным приложением

```csharp
using System;
using System.Collections.Generic;

namespace PhoneBookViewer
{
    // Модель записи телефонной книги
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; } // nullable, если email не обязателен

        public Contact(string name, string phone, string? email = null)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }

        // Форматированный вывод одной записи
        public override string ToString()
        {
            return $"Имя: {Name}, Телефон: {Phone}" +
                   (string.IsNullOrEmpty(Email) ? "" : $", Email: {Email}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Заполняем тестовые данные (в реальности можно загружать из файла/БД)
            var phoneBook = new List<Contact>
            {
                new Contact("Анна", "+79001112233", "anna@example.com"),
                new Contact("Борис", "+79004445566"),
                new Contact("Виктория", "+79007778899", "vika@example.org")
            };

            Console.WriteLine("=== Телефонная книга ===\n");

            if (phoneBook.Count == 0)
            {
                Console.WriteLine("Записей нет.");
                return;
            }

            foreach (var contact in phoneBook)
            {
                Console.WriteLine(contact);
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
```

---

## Что делает программа

- Определяет класс `Contact` с полями: имя, телефон, email.
- Создаёт список контактов и заполняет его тестовыми данными.
- Выводит каждую запись в читаемом формате.
- Обрабатывает случай, когда список пуст.

---

## Как запустить

1. Создайте консольный проект: `dotnet new console -n PhoneBookViewer`.
2. Замените содержимое `Program.cs` на код выше.
3. Запустите: `dotnet run`.

Если скажете, где должны храниться контакты (например, в JSON-файле, SQLite или CSV) или какой интерфейс нужен (WinForms, WPF, ASP.NET), я адаптирую программу под этот вариант. 😊

---

## Источник

Пример подготовлен в диалоге с Yandex Alice: [обсуждение в чате](https://alice.yandex.ru/?share=da7dd70d-fc76-8f54-b06b-465c1ec55e4f).
