using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore.Storage.Json;

public class AddBook        // class => add book => Create => CRUD
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nAdd new Book to Library.\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Enter Title: ");
                Console.ResetColor();
                var _title = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(_title))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou must enter a Title for the Book!");
                    Console.ResetColor();
                    return;
                }
            
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nEnter Year Published (yyyy):");
                Console.ResetColor();
                var _yearPublished = Console.ReadLine()?.Trim();

                if (!int.TryParse(_yearPublished, out int YearPublished))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nFormat incorrect. Try again: (yyyy)");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nConfirm adding the book '{_title}' published in {YearPublished} (y/n):");
                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nAction canceled.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nThe Book is added and ready to members!");
                Console.ResetColor();

                var _book = new Book
                {
                    Title = _title,
                    YearPublished = YearPublished,
                    IsAvailable = true
                };

                context.Books.Add(_book);
                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nThe Book: {_title} have been added to the library!");
                Console.ResetColor();
        }
    }
}
public class AddAuthor
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nAdd new Author to library:\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            var _firstName = GetInput("Enter First Name:");
            var _lastName = GetInput("Enter Last Name:");
            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(_firstName) || string.IsNullOrWhiteSpace(_lastName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFirst Name and Last Name cannot be empty. Try again.");
                Console.ResetColor();
                return;
            }

            if (context.Authors.Any(a => a.FirstName == _firstName && a.LastName == _lastName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThis author already exists in the library.");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nConfirm adding the author '{_firstName} {_lastName}' (y/n):");
            Console.ResetColor();
            var confirmation = Console.ReadLine()?.Trim().ToLower();
            if (confirmation != "y")
            {   Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAction canceled.");
                Console.ResetColor();
                return;
            }

            var _author = new Author
            {
                FirstName = _firstName,
                LastName = _lastName
            };

            context.Authors.Add(_author);
            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nThe Author '{_firstName} {_lastName}' has been added to the library!");
            Console.ResetColor();

            string GetInput(string message)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
                Console.ResetColor();
                return Console.ReadLine()?.Trim();
            }
        }
    }
}

