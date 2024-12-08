using System;
using LibrarySystem_DanielR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class RemoveBook      // class to delete => targeted data => Library => CRUD => Delete
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var _books = context.Books.ToList();

            foreach (var item in _books)
            {
                System.Console.WriteLine($"Title: {item.Title}, Published: {item.YearPublished} ID: {item.BookId}");
            }
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.WriteLine("\nEnter ID for the book you want to remove!");
            Console.ResetColor();

            var _input = Console.ReadLine();

            if (!int.TryParse(_input, out var bookID))
            {
                System.Console.WriteLine("ID not found, please try again!");
            }

            var removeBook = context.Books.FirstOrDefault(b => b.BookId == bookID);

            if (removeBook == null)
            {
                System.Console.WriteLine("ID not found, please try again!");
                return;
            }

            var matchedBookAuthor = context.BookAuthors
            .Where(ba => ba.BookID == bookID)
            .ToList();

            if (matchedBookAuthor.Any())
            {
                Console.WriteLine("Warning - Book has connection to multiple Authors and will be removed!");

                context.BookAuthors.RemoveRange(matchedBookAuthor);

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }

            context.Books.Remove(removeBook);
            context.SaveChanges();

            Console.WriteLine($"You have now erased following Book: {removeBook.Title}.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}

public class RemoveAuthor
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var _author = context.Authors.ToList();

            foreach (var item in _author)
            {
                Console.WriteLine($"Author ID: {item.AuthorId}, Name: {item.FirstName} {item.LastName}.");
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nEnter ID for the Author you want to remove:");
            Console.ResetColor();

            var _input = Console.ReadLine()?.Trim();

            if (!int.TryParse(_input, out var authorID))
            {
                Console.WriteLine("ID not found, please try again!");
                return;
            }

            var removeAuthor = context.Authors.FirstOrDefault(a => a.AuthorId == authorID);
            
            if (removeAuthor == null)
            {
                Console.WriteLine("ID not found, please try again!");
                return;
            }

            var matchedBookAuthor = context.BookAuthors
            .Where(ba => ba.AuthorID == authorID)
            .ToList();

            if (matchedBookAuthor.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning - Author has connection to multiple books and will also be removed!");
                Console.ResetColor();

                context.BookAuthors.RemoveRange(matchedBookAuthor);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nPress any key to continue...");
                Console.ResetColor();
                Console.ReadLine();
            }

            context.Authors.Remove(removeAuthor);
            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nYou have now erased following Author: {removeAuthor.FirstName} {removeAuthor.LastName}.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            
        }
    }
}

