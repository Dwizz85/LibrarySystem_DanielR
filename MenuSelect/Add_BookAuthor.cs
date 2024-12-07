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
                    System.Console.WriteLine("Format incorrect. Try again: (yyyy-mm-dd)");
                    return;
                }

                if (YearPublished.Year > DateTime.Now.Year)
                {
                    Console.WriteLine("Year Published cannot be in the future. Try again.");
                    return;
                }

            Console.WriteLine($"Confirm adding the book '{_title}' published in {YearPublished} (y/n):");
            var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.WriteLine("Action canceled.");
                    return;
                }

            System.Console.WriteLine("The Book is added and ready to members!");

            var _book = new Book
            {
                Title = _title,
                YearPublished = YearPublished,
                IsAvailable = true
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
            Console.WriteLine("\nAdd new Author to library:\n");

            var _firstName = GetInput("Enter First Name:");
            var _lastName = GetInput("Enter Last Name:");

            if (string.IsNullOrWhiteSpace(_firstName) || string.IsNullOrWhiteSpace(_lastName))
            {
                Console.WriteLine("First Name and Last Name cannot be empty. Try again.");
                return;
            }

            if (context.Authors.Any(a => a.FirstName == _firstName && a.LastName == _lastName))
            {
                Console.WriteLine("This author already exists in the library.");
                return;
            }

            Console.WriteLine($"Confirm adding the author '{_firstName} {_lastName}' (y/n):");
            var confirmation = Console.ReadLine()?.Trim().ToLower();
            if (confirmation != "y")
            {
                Console.WriteLine("Action canceled.");
                return;
            }

            var _author = new Author
            {
                FirstName = _firstName,
                LastName = _lastName
            };

            context.Authors.Add(_author);
            context.SaveChanges();
            Console.WriteLine($"The Author '{_firstName} {_lastName}' has been added to the library!");

            string GetInput(string message)
            {
                Console.WriteLine(message);
                return Console.ReadLine()?.Trim();
            }
        }
    }
}

