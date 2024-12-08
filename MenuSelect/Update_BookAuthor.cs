using System;
using Microsoft.EntityFrameworkCore;
using LibrarySystem_DanielR;

public class UpdateBook // Class for update => Book => CRUD => Update
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true) // Loop for retrying
            {
                Console.Clear(); // Clear console for better readability

                var books = context.Books.ToList();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nUpdate Book Information:\n");
                Console.ResetColor();

                if (!books.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNo books found in the library.");
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                // Display books with formatted table
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ID     Title                            Year Published     Available");
                Console.WriteLine("------------------------------------------------------------------");
                Console.ResetColor();

                foreach (var book in books)
                {
                    var truncatedTitle = book.Title.Length > 30 ? book.Title.Substring(0, 27) + "..." : book.Title;
                    Console.WriteLine($"{book.BookId,-6} {truncatedTitle,-30} {book.YearPublished,-20} {(book.IsAvailable ? "Yes" : "No")}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the ID of the book you want to update (or press Enter to cancel): ");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Returning to menu...");
                    Console.ResetColor();
                    return;
                }

                if (!int.TryParse(input, out var bookId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. To try again press any key.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry
                }

                var updateBook = context.Books.FirstOrDefault(b => b.BookId == bookId);

                if (updateBook == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nBook ID not found. To try again press any key.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\nCurrent Title: {updateBook.Title}");
                Console.WriteLine("\nEnter new Title (or press Enter to keep the current title):");
                Console.ResetColor();

                var newTitle = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    updateBook.Title = newTitle;
                }

                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nBook updated: {updateBook.Title}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return...");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
        }
    }
}


public class UpdateAuthor // Class for update => Author => CRUD => Update
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true) // Loop for retrying
            {
                Console.Clear(); // Clear console for better readability

                var authors = context.Authors.ToList();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nUpdate Author Information:\n");
                Console.ResetColor();

                if (!authors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No authors found in the library.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    return;
                }

                // Display authors with formatted table
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ID     First Name                Last Name");
                Console.WriteLine("------------------------------------------");
                Console.ResetColor();

                foreach (var author in authors)
                {
                    var firstName = author.FirstName.Length > 20 ? author.FirstName.Substring(0, 17) + "..." : author.FirstName; // Truncate if too long
                    var lastName = author.LastName.Length > 20 ? author.LastName.Substring(0, 17) + "..." : author.LastName; // Truncate if too long

                    Console.WriteLine($"{author.AuthorId,-6} {firstName,-20} {lastName}");
}


                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the ID of the author you want to update (or press Enter to cancel): ");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nAction canceled. Press any key to continue.");
                        Console.ResetColor();
                        Console.ReadKey();
                        return;
                    }

                    if (!int.TryParse(input, out var authorId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid input. Please enter a valid numeric Author ID.");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nPress any key to try again...");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue; // Retry
                    }

                var updateAuthor = context.Authors.FirstOrDefault(a => a.AuthorId == authorId);

                    if (updateAuthor == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nAuthor ID not found. To try again press any key.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue; // Retry
                    }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\nCurrent Name: {updateAuthor.FirstName} {updateAuthor.LastName}");
                Console.WriteLine("\nEnter new First Name (or press Enter to keep the current name):");
                Console.ResetColor();

                var newFirstName = Console.ReadLine()?.Trim();

                    if (!string.IsNullOrWhiteSpace(newFirstName))
                    {
                        updateAuthor.FirstName = newFirstName;
                    }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nEnter new Last Name (or press Enter to keep the current name):");
                Console.ResetColor();

                var newLastName = Console.ReadLine()?.Trim();

                    if (!string.IsNullOrWhiteSpace(newLastName))
                    {
                        updateAuthor.LastName = newLastName;
                    }

                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nAuthor updated: {updateAuthor.FirstName} {updateAuthor.LastName}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return...");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
        }
    }
}

