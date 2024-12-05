using System;
using Microsoft.EntityFrameworkCore;
using LibrarySystem_DanielR;

public class UpdateBook             // Class for update => Book => CRUD => Update
{

    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var Books = context.Books.ToList();
            System.Console.WriteLine("Enter Book ID to update: ");

            if (!int.TryParse(Console.ReadLine(), out var bookID))
            {
                System.Console.WriteLine("No ID found, please try again.");

                foreach (var _book in Books)
                {
                    System.Console.WriteLine($"Title: {_book.Title}, => ID: {_book.BookId}.");
                }
                return;
            }
            var updateBook = context.Books.Find(bookID);
            System.Console.WriteLine($"Book title is: {updateBook.Title}.\n Enter new Book Title: ");
            var _title = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(_title))
            {
                updateBook.Title = _title;
            }
            context.SaveChanges();
            System.Console.WriteLine($"New Title for Book is updated: {_title}.");
        }
    }
}

public class UpdateAuthor
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var Authors = context.Authors.ToList();
            System.Console.WriteLine("Enter Author ID: ");

            if (!int.TryParse(Console.ReadLine(), out var AuthorID))
            {
                foreach (var _author in Authors)
                {  
                   System.Console.WriteLine($"Author: {_author.FirstName} {_author.LastName} with ID: {_author.AuthorId}"); 
                }
                return;
            }

            var updateAuthor = context.Authors.Find(AuthorID);
            System.Console.WriteLine($"Author name: {updateAuthor.FirstName} {updateAuthor.LastName}");

            var _firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(_firstName))
            {
                updateAuthor.FirstName = _firstName;
            }

            var _lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(_lastName))
            {
                updateAuthor.LastName = _lastName;
            }
            context.SaveChanges();
            System.Console.WriteLine("Author is updated: {_firstName} {_lastName}.");
        }
    }
}