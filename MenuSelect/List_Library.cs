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

            System.Console.WriteLine("Library Listing => Author with Books:");
            foreach (var _book in BookAuthor)
            {
                var _authors = _book.BookAuthors.Any()
                ? string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"))
                : "No Authors";

                System.Console.WriteLine($"Book: {_book.Title} Author: {_authors}");
                
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
                Console.WriteLine($"{"Title:",-15} {item.Title}");
                Console.WriteLine($"{"Member ID:",-15} {item.MemberID}");
                Console.WriteLine($"{"Loan Date:",-15} {item.LoanDate:yyyy-MM-dd}");
                Console.WriteLine($"{"Return Date:",-15} {item.ReturnDate}");
                Console.WriteLine($"{"Is Returned:",-15} {(item.IsReturned ? "Yes" : "No")}");
                Console.WriteLine(new string('-', 40)); // Separator between record
            }
            
            System.Console.WriteLine("Press any key to return to Menu");
        }

        Console.ReadLine();
    }
}