# KodikChat

Учебное консольное приложение на C#: хранит контакты и выводит телефонную книгу в консоль.

## Возможности

- класс `Contact` хранит имя и номер телефона;
- список `phoneBook` содержит три контакта;
- цикл `foreach` выводит записи в формате `Имя: телефон`.

## Исходный код

```csharp
using System;
using System.Collections.Generic;

class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public Contact(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }
}

class Program
{
    static void Main()
    {
        var phoneBook = new List<Contact>
        {
            new Contact("Анна Иванова", "+7 900 123-45-67"),
            new Contact("Иван Петров", "+7 901 234-56-78"),
            new Contact("Мария Сидорова", "+7 902 345-67-89")
        };

        Console.WriteLine("Телефонная книга:");

        foreach (Contact contact in phoneBook)
        {
            Console.WriteLine($"{contact.Name}: {contact.Phone}");
        }
    }
}
```

Список `phoneBook` содержит записи телефонной книги, а цикл `foreach` выводит каждую из них.

## Ожидаемый вывод

```text
Телефонная книга:
Анна Иванова: +7 900 123-45-67
Иван Петров: +7 901 234-56-78
Мария Сидорова: +7 902 345-67-89
```

## Запуск

Для запуска нужен .NET SDK. В текущей папке нет файла проекта `.csproj`, поэтому `dotnet run` сразу не сработает. После создания консольного проекта:

```bash
dotnet run
```

## Примечание

Ссылка на чат не предоставляется.
