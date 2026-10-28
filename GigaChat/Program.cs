using System;
using System.Collections.Generic;

// Структура (в данном случае класс) для хранения данных одного контакта
public class Contact
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public Contact(string name, string phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }

    // Переопределение метода ToString() для удобного вывода информации о контакте
    public override string ToString()
    {
        return $"Имя: {Name}, Телефон: {PhoneNumber}";
    }
}

class Program
{
    // Список для хранения всех записей телефонной книги
    private static List<Contact> phoneBook = new List<Contact>();

    static void Main(string[] args)
    {
        // Добавим несколько тестовых записей для наглядности
        phoneBook.Add(new Contact("Иван Петров", "+7 (999) 123-45-67"));
        phoneBook.Add(new Contact("Мария Сидорова", "+7 (911) 765-43-21"));

        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Телефонная книга ===");
            Console.WriteLine("1. Показать все записи");
            Console.WriteLine("2. Добавить новую запись");
            Console.WriteLine("3. Найти запись по имени");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите действие (1-4): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowAllContacts();
                    break;
                case "2":
                    AddContact();
                    break;
                case "3":
                    SearchContact();
                    break;
                case "4":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Нажмите Enter для продолжения.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    // Метод для показа всех записей
    private static void ShowAllContacts()
    {
        if (phoneBook.Count == 0)
        {
            Console.WriteLine("Телефонная книга пуста.");
        }
        else
        {
            Console.WriteLine("Список всех контактов:");
            // Цикл foreach для обхода всех элементов списка
            foreach (var contact in phoneBook)
            {
                Console.WriteLine(contact);
            }
        }
        
        Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню.");
        Console.ReadLine();
    }

    // Метод для добавления новой записи
    private static void AddContact()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите номер телефона: ");
        string phone = Console.ReadLine();

        phoneBook.Add(new Contact(name, phone));
        Console.WriteLine("Запись успешно добавлена. Нажмите Enter.");
        Console.ReadLine();
    }

    // Метод для поиска записи по имени
    private static void SearchContact()
    {
        Console.Write("Введите имя для поиска: ");
        string searchName = Console.ReadLine();
        
        bool found = false;
        foreach (var contact in phoneBook)
        {
            // StringComparison.OrdinalIgnoreCase делает поиск нечувствительным к регистру
            if (contact.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Найдена запись: " + contact);
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Контакт с таким именем не найден.");
        }
        
        Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню.");
        Console.ReadLine();
    }
}
