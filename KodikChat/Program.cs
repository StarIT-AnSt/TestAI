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
