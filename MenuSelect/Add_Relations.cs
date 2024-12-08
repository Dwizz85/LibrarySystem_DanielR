using System;
using LibrarySystem_DanielR;

public class AddRelations   // Class to set relation => Book => Author
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true) // Allow retries for invalid inputs
            {
                Console.Clear(); // Clear the console for better readability

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nAdd Relations between Books and Authors:\n");
                Console.ResetColor();

                // Display all books with a formatted table
                var books = context.Books.ToList();

                if (!books.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No books found in the library.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Book ID  Title                            Year Published");
                Console.WriteLine("-------------------------------------------------------");
                Console.ResetColor();

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.BookId,-8} {book.Title,-30} {book.YearPublished,-15}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the Book ID to add a relation (or press Enter to cancel): ");
                Console.ResetColor();

                var bookInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(bookInput))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Returning to menu...");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                if (!int.TryParse(bookInput, out var bookID) || !books.Any(b => b.BookId == bookID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Book ID. Please try again.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to retry...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                // Display all authors with a formatted table
                Console.Clear();
                var authors = context.Authors.ToList();

                if (!authors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No authors found in the library.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Author ID  Name");
                Console.WriteLine("-------------------------------");
                Console.ResetColor();

                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.AuthorId,-10} {author.FirstName} {author.LastName}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the Author ID to add a relation (or press Enter to cancel): ");
                Console.ResetColor();

                var authorInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(authorInput))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Returning to menu...");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                if (!int.TryParse(authorInput, out var authorID) || !authors.Any(a => a.AuthorId == authorID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Author ID. Please try again.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to retry...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                // Create the relationship
                var bookAuthor = new BookAuthor { BookID = bookID, AuthorID = authorID };

                try
                {
                    context.BookAuthors.Add(bookAuthor);
                    context.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\nSuccessfully created a relation between Book ID: {bookID} and Author ID: {authorID}.");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nAn error occurred while saving the relationship: {ex.Message}");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return...");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
        }
    }
}
