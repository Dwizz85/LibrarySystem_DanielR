using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;


public class ListLibrary // Class => Lists Books and Authors
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.Clear();

            // Join Authors & Books
            var bookAuthors = context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .ToList();

            if (!bookAuthors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo books or authors found in the library.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nListing Books with Authors in Library:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ID       Title                           Year Published     Authors");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.ResetColor();


                foreach (var book in bookAuthors)
                {
                    var authors = book.BookAuthors.Any()
                        ? string.Join(", ", book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"))
                        : "No Authors";

                    Console.WriteLine($"{book.BookId,-8} {book.Title,-30} {book.YearPublished,-20} {authors}");
                }
                

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
            Console.ReadKey();
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

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nLoan History:");
                    Console.ResetColor();

                        foreach (var item in loanHistory)
                        {
                            Console.WriteLine(new string('-', 40)); // Separator between record
                            Console.WriteLine($"{"Title:",-15} {item.Title}");
                            Console.WriteLine($"{"Member ID:",-15} {item.MemberID}");
                            Console.WriteLine($"{"Loan Date:",-15} {item.LoanDate:yyyy-MM-dd}");
                            Console.WriteLine($"{"Return Date:",-15} {item.ReturnDate}");
                            Console.WriteLine($"{"Is Returned:",-15} {(item.IsReturned ? "Yes" : "No")}");
                        }
        }
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


            Console.ForegroundColor = ConsoleColor.Yellow;
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
            Console.Clear();

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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book ID  Title                            Year Published  Available");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.ResetColor();

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.BookId,-8} {book.Title,-30} {book.YearPublished,-20} {(book.IsAvailable ? "Yes" : "No")}");
                }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}

public class BooksByAuthorListing // Class to list books by a specific author
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.Clear();

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

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nAuthors Available:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ID       Name");
            Console.WriteLine("-------------------------------");
            Console.ResetColor();

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.AuthorId,-8} {author.FullName}");
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\nEnter the Author ID to view their books or press Enter to cancel: ");
            Console.ResetColor();

            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAction canceled. Returning to menu...");
                Console.ResetColor();
                return;
            }

            if (!int.TryParse(input, out var authorId))
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
                    ba.Book.BookId,
                    ba.Book.Title,
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book ID  Title                            Year Published  Available");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.ResetColor();

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.BookId,-8} {book.Title,-30} {book.YearPublished,-20} {(book.IsAvailable ? "Yes" : "No")}");
                }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}


public class AuthorsByBookListing       // Class to list books with more than one author
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.Clear();

            // Fetch books with more than one author
            var booksWithMultipleAuthors = context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Where(b => b.BookAuthors.Count > 1) // Filter for books with more than one author
                .OrderBy(b => b.Title)
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.YearPublished
                })
                .ToList();

            if (!booksWithMultipleAuthors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo books with multiple authors found in the library.");
                Console.ResetColor();
                return;
            }

           Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nBooks with Multiple Authors:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ID       Title                            Year Published");
            Console.WriteLine("-------------------------------------------------------");
            Console.ResetColor();

            foreach (var book in booksWithMultipleAuthors)
            {
                Console.WriteLine($"{book.BookId,-8} {book.Title,-30} {book.YearPublished,-15}");
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\nEnter the Book ID to view its authors (or press Enter to cancel): ");
            Console.ResetColor();

            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nAction canceled. Press any key to return to the menu.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            if (!int.TryParse(input, out var bookId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Book ID. Press any key to return to the menu.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            // Fetch authors for the selected book
            var authors = context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .Where(ba => ba.BookID == bookId)
                .Select(ba => new
                {
                    ba.Author.AuthorId,
                    ba.Author.FirstName,
                    ba.Author.LastName
                })
                .ToList();

            if (!authors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo authors found for the selected book.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nAuthors for Book ID: {bookId}:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Author ID  Name");
            Console.WriteLine("-------------------------------");
            Console.ResetColor();

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.AuthorId,-8} {author.FirstName} {author.LastName}");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
