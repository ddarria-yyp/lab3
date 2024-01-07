using System.Drawing;
using System;
using System.IO;
using System.Numerics;
using System.Resources;
using System.Text.Json;
using BooksCatalog;
using Newtonsoft.Json;

internal class Program
{
    static void Main(string[] args)
    {
        BookCatalogController bookCatalogController = new BookCatalogController();
        Start(bookCatalogController);
        bookCatalogController.Save();
    }

    private static void Start(BookCatalogController bookCatalogController)
    {
        Console.WriteLine(
            "Выберите команду: 1 - добавить книгу, 2 - найти книгу, 3 - настройки сохранения, 4 - вернуться в главное меню, любое другое - выход");
        string n = Console.ReadLine();
        if (n == "1")
        {
            AddBook(bookCatalogController);
        }
        else if (n == "2")
        {
            FindBook(bookCatalogController);
        }
        else if (n == "3")
        {
            ChooseSaving(bookCatalogController);
        }
        else if (n == "4")
        {
            Start(bookCatalogController);
        }
    }

    private static void ChooseSaving(BookCatalogController bookCatalogController)
    {
        Console.WriteLine("Выберите типа cохранения каталога (по умолчанию json) : 1 - json, 2 - xml, 3 - sqLite");
        string n = Console.ReadLine();
        int.TryParse(n, out int savingTypeNumber);
       
        if (Enum.IsDefined(typeof(SavingType), savingTypeNumber))
        {
            SavingType savingType = (SavingType)savingTypeNumber;
            string json = JsonConvert.SerializeObject(savingType);
            File.WriteAllText("settings.json", json);
            Console.WriteLine("Настройки сохранены");
            Start(bookCatalogController);
        }
        else
        {
            Console.WriteLine("Введите валидное число");
            ChooseSaving(bookCatalogController);
        }
    }

    private static void FindBook(BookCatalogController bookCatalogController)
    {
        Console.WriteLine(
            "Выберите команду: 1 - найти по названию, 2 - найти по ISBN, 3 - найти по автору, 4 - найти по ключевым словам");
        string n = Console.ReadLine();
        if (n == "1")
        {
            Search("Название", Category.Title, bookCatalogController);
        }
        else if (n == "2")
        {
            Search("ISBN", Category.ISBN, bookCatalogController);
        }
        else if (n == "3")
        {
            Search("Автора", Category.Author, bookCatalogController);

        }
        else if (n == "4")
        {
            Search("Ключевые слова через запятую", Category.Keyword, bookCatalogController);

        }

        Start(bookCatalogController);
    }

    private static void Search(string name, Category findByEnum, BookCatalogController bookCatalogController)
    {
        Console.WriteLine($"Введите {name}");
        string value = Console.ReadLine();
        var books = bookCatalogController.FindBooks(findByEnum, value);
        PrintBooks(books);
    }

    private static void PrintBooks(IEnumerable<Book> books)
    {
        if (!books.Any())
        {
            Console.WriteLine("Ничего не найдено");
        }
        else
        {
            Console.WriteLine("Найденные результаты.");
        }
       
        foreach (Book book in books)
        {
            Console.WriteLine($"Название: {book.Title}, Автор:{book.Author},  ISBN: {book.ISBN},  Дата публикации:{book.Date}, Жанр:{book.Genre}");
        }
    }

    private static string GetFromUser(string title)
    {
        Console.WriteLine($"Введите {title}");
        return Console.ReadLine();
    }

    private static DateTime GetPublicationDate()
    {
        string date = GetFromUser("дату публикации");

        if (!DateTime.TryParse(date, out DateTime pubication))
        {
            Console.WriteLine("Введите валидную дату");
            return GetPublicationDate();
        }
        else
        {
            return pubication;
        }
    }
    private static void AddBook(BookCatalogController bookCatalogController)
    {
        string title = GetFromUser("название");
        string author = GetFromUser("автора");
        string isbn = GetFromUser("ISBN");
        string brief = GetFromUser("аннатоцию");
        string genre = GetFromUser("жанр");
        DateTime date = GetPublicationDate();
        bookCatalogController.AddBook(title, author, isbn, brief, date, genre);
        Start(bookCatalogController);
    }
}