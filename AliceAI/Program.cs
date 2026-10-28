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
