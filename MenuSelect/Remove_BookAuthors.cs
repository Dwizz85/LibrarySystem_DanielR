using System;
using LibrarySystem_DanielR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class RemoveBook // Class to delete => targeted data => Library => CRUD => Delete
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true) // Loop to allow retrying
            {
                Console.Clear(); // Clear the console for readability

                var _books = context.Books.ToList();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nRemove Book from Library:\n");
                Console.ResetColor();

                if (!_books.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No books found in the library.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    return; // Exit if no books exist
                }

                // Display books with formatting
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ID     Title                            Year Published");
                Console.WriteLine("----------------------------------------------------");
                Console.ResetColor();

                foreach (var item in _books)
                {
                    // Truncate titles longer than 30 characters
                    var truncatedTitle = item.Title.Length > 30 ? item.Title.Substring(0, 27) + "..." : item.Title;
                    Console.WriteLine($"{item.BookId,-6} {truncatedTitle,-30} {item.YearPublished}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the ID of the book you want to remove (or press Enter to cancel): ");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Returning to menu...");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    return; // Exit the function if canceled
                }

                if (!int.TryParse(input, out var bookID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a valid numeric Book ID.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                var removeBook = context.Books.FirstOrDefault(b => b.BookId == bookID);

                if (removeBook == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBook ID not found. Please try again.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                // Check for related BookAuthor entries
                var matchedBookAuthor = context.BookAuthors
                    .Where(ba => ba.BookID == bookID)
                    .ToList();

                if (matchedBookAuthor.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWarning - This book is linked to one or more authors and will be removed along with those connections.");
                    Console.ResetColor();

                    context.BookAuthors.RemoveRange(matchedBookAuthor);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to confirm removal...");
                    Console.ResetColor();
                    Console.ReadKey();
                }

                // Remove the book and save changes
                context.Books.Remove(removeBook);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nThe book '{removeBook.Title}' has been successfully removed from the library.");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return...");
                Console.ResetColor();
                Console.ReadKey();
                return; // Exit after successful removal
            }
        }
    }
}

public class RemoveAuthor
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true) // Loop to allow retrying
            {
                Console.Clear(); // Clear the console for better readability

                var _authors = context.Authors.ToList();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nRemove Author from Library:\n");
                Console.ResetColor();

                if (!_authors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No authors found in the library.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit if no authors exist
                }

                // Display authors with formatted table
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ID     Name");
                Console.WriteLine("-------------------------------");
                Console.ResetColor();

                foreach (var item in _authors)
                {
                    Console.WriteLine($"{item.AuthorId,-6} {item.FirstName} {item.LastName}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the ID of the author you want to remove (or press Enter to cancel): ");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nAction canceled. Returning to menu...");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit the function if canceled
                }

                if (!int.TryParse(input, out var authorID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a valid numeric Author ID.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                var removeAuthor = context.Authors.FirstOrDefault(a => a.AuthorId == authorID);

                if (removeAuthor == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nAuthor ID not found. Please try again.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                // Check for related BookAuthor entries
                var matchedBookAuthor = context.BookAuthors
                    .Where(ba => ba.AuthorID == authorID)
                    .ToList();

                if (matchedBookAuthor.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nWarning - This author is linked to one or more books and the connections will also be removed.");
                    Console.ResetColor();

                    context.BookAuthors.RemoveRange(matchedBookAuthor);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to confirm removal...");
                    Console.ResetColor();
                    Console.ReadKey();
                }

                // Remove the author and save changes
                context.Authors.Remove(removeAuthor);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nThe author '{removeAuthor.FirstName} {removeAuthor.LastName}' has been successfully removed.");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return...");
                Console.ResetColor();
                Console.ReadKey();
                return; // Exit after successful removal
            }
        }
    }
}


