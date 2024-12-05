using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class LoanBook   // class to loan => book => relate => member
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            System.Console.WriteLine("Books available for Loan:");
            var Books = context.Books
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .ToList();
        
        if (!Books.Any())
        {
            System.Console.WriteLine("No books available for loan at this moment!");
            return;
        }

        foreach (var _book in Books)
        {
            var authors = string.Join(",", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
            System.Console.WriteLine($"Book ID: {_book.BookId, -10} Title: {_book.Title, -40} Authors: {authors}");   
        }
        }
    }


}