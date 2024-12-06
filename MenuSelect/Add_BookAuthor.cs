using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore.Storage.Json;

public class AddBook        // class => add book => Create => CRUD
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            System.Console.WriteLine("\nAdd new Book to Library.\n");

            System.Console.WriteLine("Enter Title: ");
            var _title = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(_title))
            {
                System.Console.WriteLine("You must enter a Title for the Book!");
                return;
            }
            
            System.Console.WriteLine("Enter Year Published (yyyy): ");
            var _yearPublished = Console.ReadLine()?.Trim();

            if (!DateOnly.TryParse(_yearPublished, out DateOnly YearPublished))
            {
                System.Console.WriteLine("Format incorrect. Try again: (yyyy)");
                return;
            }

            System.Console.WriteLine("The Book is added and ready to members!");
            bool _IsAvailable = true;

            var _book = new Book
            {
                Title = _title,
                YearPublished = YearPublished,
                IsAvailable = _IsAvailable
            };

            context.Books.Add(_book);
            context.SaveChanges();
            System.Console.WriteLine($"The Book: {_title} have been added to the library!");
        }
    }
}
public class AddAuthor
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            System.Console.WriteLine("\nAdd new Author to library:\n");

            System.Console.WriteLine("Enter First Name: ");
            var _firstName = Console.ReadLine()?.Trim();

            System.Console.WriteLine("Enter Last Name: ");
            var _lastName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(_firstName) || string.IsNullOrWhiteSpace(_lastName))
            {
                System.Console.WriteLine("First Name and Last Name cannot be empty. Try again.");
                return;
            }

            var _author = new Author
            {
                FirstName =_firstName,
                LastName = _lastName,

            };

            context.Authors.Add(_author);
            context.SaveChanges();
            System.Console.WriteLine($"The Author: {_firstName} {_lastName}, have been added to library!");

        }
    }
}
