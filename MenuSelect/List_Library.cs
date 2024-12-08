using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ListLibrary            // class => lists => content => library
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var BookAuthor = context.Books      // join Authors & Books
            .Include(ba => ba.BookAuthors)
            .ThenInclude(a => a.Author)
            .ToList();
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("\nListing Books with Authors in Library:\n");
            Console.ResetColor();
            foreach (var _book in BookAuthor)
            {
                var _authors = _book.BookAuthors.Any()
                ? string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"))
                : "No Authors";

                System.Console.WriteLine($"Book ID:{_book.BookId,-10} Title: {_book.Title,-40} Year Published: {_book.YearPublished,-10} Author: {_authors,-10}");
                
            }
        }
    }
}

public class LoanHistory
{
    public static void Run()
    {

        using (var context = new AppDbContext())
        {
            var loanHistory = context.Loans
                .Include(l => l.Book)
                .ThenInclude(b => b.BookAuthors)
                .Include(l => l.Member)
                .Select(lh => new
                {
                    lh.Book.Title,
                    lh.Member.MemberID,
                    lh.LoanDate,
                    ReturnDate = lh.ReturnDate.ToString("yyyy-MM-dd") ?? "Not Returned",
                    lh.IsReturned
                })
                .ToList();

            foreach (var item in loanHistory)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine("\nLoan History:\n");
                Console.ResetColor();
                Console.WriteLine($"\n{"Title:",-15} {item.Title}");
                Console.WriteLine($"{"Member ID:",-15} {item.MemberID}");
                Console.WriteLine($"{"Loan Date:",-15} {item.LoanDate:yyyy-MM-dd}");
                Console.WriteLine($"{"Return Date:",-15} {item.ReturnDate}");
                Console.WriteLine($"{"Is Returned:",-15} {(item.IsReturned ? "Yes" : "No")}");
                Console.WriteLine(new string('-', 40)); // Separator between record
            }
            
        }

        Console.ReadLine();
    }
}

public class ActiveLoans
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            // Fetch only active loans where IsReturned is false
            var activeLoans = context.Loans
                .Include(l => l.Book)
                .ThenInclude(b => b.BookAuthors)
                .Include(l => l.Member)
                .Where(l => !l.IsReturned) // Filter for active loans
                .Select(lh => new
                {
                    lh.Book.Title,
                    lh.Member.MemberID,
                    lh.LoanDate
                })
                .ToList();

            // Check if there are active loans
            if (!activeLoans.Any())
            {   
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo active loans at the moment.");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nActive Loans:\n");
            Console.ResetColor();

            // Display active loans
            foreach (var loan in activeLoans)
            {
                Console.WriteLine($"{"Title:",-15} {loan.Title}");
                Console.WriteLine($"{"Member ID:",-15} {loan.MemberID}");
                Console.WriteLine($"{"Loan Date:",-15} {loan.LoanDate:yyyy-MM-dd}");
                Console.WriteLine(new string('-', 40)); // Separator between records
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
        }

        Console.ReadLine();
    }
}

public class BooksOnlyListing // Class to list only books
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            // Fetch all books
            var books = context.Books
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.YearPublished,
                    b.IsAvailable
                })
                .ToList();

            if (!books.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo books found in the library.");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nListing Books in Library:\n");
            Console.ResetColor();

            foreach (var book in books)
            {
                Console.WriteLine($"Book ID: {book.BookId,-10} Title: {book.Title,-40} Year Published: {book.YearPublished,-10} Available: {(book.IsAvailable ? "Yes" : "No")}");
            }

        }
    }
}

public class BooksByAuthorListing // Class to list books by a specific author
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            // Fetch all authors
            var authors = context.Authors
                .OrderBy(a => a.LastName)
                .Select(a => new
                {
                    a.AuthorId,
                    FullName = $"{a.FirstName} {a.LastName}"
                })
                .ToList();

            if (!authors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo authors found in the library.");
                Console.ResetColor();
                return;
            }

            // Display list of authors
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nAuthors Available:\n");
            Console.ResetColor();
            foreach (var author in authors)
            {
                Console.WriteLine($"Author ID: {author.AuthorId,-10} Name: {author.FullName}");
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nEnter the Author ID to view their books or press Enter to return:");
            Console.ResetColor();
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Returning to menu...");
                Console.ResetColor();
                return;
            }

            if (!int.TryParse(input, out int authorId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Author ID. Returning to menu...");
                Console.ResetColor();
                return;
            }

            // Fetch books for the selected author
            var books = context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .Where(ba => ba.AuthorID == authorId)
                .Select(ba => new
                {
                    ba.Book.Title,
                    ba.Book.BookId,
                    ba.Book.YearPublished,
                    ba.Book.IsAvailable
                })
                .ToList();

            if (!books.Any())
            {   
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo books found for the selected author.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nBooks by Author ID: {authorId}:\n");
            Console.ResetColor();

            foreach (var book in books)
            {
                Console.WriteLine($"Book ID: {book.BookId,-10} Title: {book.Title,-40} Year Published: {book.YearPublished,-10} Available: {(book.IsAvailable ? "Yes" : "No")}");
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
        }

        Console.ReadKey();
    }
}