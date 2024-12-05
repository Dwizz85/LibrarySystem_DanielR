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
                var _authors = string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
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
            var LoanHistory = context.Loans
            .Include(l => l.Book)
            .ThenInclude(ba => ba.BookAuthors)
            .Select(lh => new
            {
                lh.Book.Title,
                lh.Member.MemberID,
                lh.LoanDate,
                lh.ReturnDate,
                lh.IsReturned

            }).ToList();
            foreach (var item in LoanHistory)
            {
                System.Console.WriteLine($"{"Title:", -10} {item.Title}");
                System.Console.WriteLine($"{"Member ID:", -10} {item.MemberID}");
                System.Console.WriteLine($"{"Loan Date:", -10} {item.LoanDate}");
                System.Console.WriteLine($"{"Return Date:", -10} {item.ReturnDate}");
                System.Console.WriteLine($"{"Is Returned", -10} {(item.IsReturned ? "Yes" : "No")}");
            }
            System.Console.WriteLine("Press any key to return to Menu");
        }
        Console.ReadLine();
    }
}