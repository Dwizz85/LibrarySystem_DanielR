using System;
using LibrarySystem_DanielR;

public class AddRelations   // class to set relation => Book => Author
{

    public static void Run()
    {
        using (var context = new AppDbContext())
        {
        System.Console.WriteLine("Add relations between Books & Authors by entereing respective ID.");

        System.Console.WriteLine("1: Enter Book ID to add relation: ");
        if(!int.TryParse(Console.ReadLine(), out int bookID))
        {
            System.Console.WriteLine("Book ID entered is not in library, please try again!");
            return;
        }

        System.Console.WriteLine("2: Enter Author ID to add relation: ");
        if(!int.TryParse(Console.ReadLine(), out int authorID))
        {
            System.Console.WriteLine("Author ID entered is not in library, please try again!");
            return;
        }

        var BookAuthor = new BookAuthor {BookID = bookID, AuthorID = authorID};

        context.BookAuthors.Add(BookAuthor);
        context.SaveChanges();
        System.Console.WriteLine("BookAuthor have a new relation!\n New relation between BookID: {bookID} and AuthorID: {authorID}. ");
        
        }
    }
}