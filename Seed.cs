using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;

// This Seed class is responsible for adding initial data to the database 
// (to ensure the program has data to work with when running)
public class Seed
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var transaction = context.Database.BeginTransaction();  // Start a database transaction

            try
            {
                // Check if any data already exists in Books, Authors, or BookAuthors tables
                if (!context.Books.Any() && !context.Authors.Any() && !context.BookAuthors.Any())
                {

                    #region Books Information
                    // Create book objects with their respective titles, publication years, and availability status    
                    var book1 = new Book
                    {
                        Title = "To Kill a Mockingbird",
                        YearPublished = new DateOnly(1960, 7, 11),
                        IsAvailable = true
                    };

                    var book2 = new Book
                    {
                        Title = "1984",
                        YearPublished = new DateOnly(1949, 6, 8),
                        IsAvailable = true
                    };

                    var book3 = new Book
                    {
                        Title = "Pride and Prejudice",
                        YearPublished = new DateOnly(1813, 1, 28),
                        IsAvailable = true
                    };

                    var book4 = new Book
                    {
                        Title = "Art of War",
                        YearPublished = new DateOnly(1452, 3, 12),
                        IsAvailable = true
                    };

                    var book5 = new Book
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        YearPublished = new DateOnly(1997, 6, 26),
                        IsAvailable = true
                    };

                    var book6 = new Book
                    {
                        Title = "The Hobbit",
                        YearPublished = new DateOnly(1937, 9, 21),
                        IsAvailable = true
                    };

                    var book7 = new Book
                    {
                        Title = "Brave New World",
                        YearPublished = new DateOnly(1932, 8, 18),
                        IsAvailable = true
                    };

                    var book8 = new Book
                    {
                        Title = "Animal Farm",
                        YearPublished = new DateOnly(1945, 8, 17),
                        IsAvailable = true
                    };

                    var book9 = new Book
                    {
                        Title = "Emma",
                        YearPublished = new DateOnly(1815, 12, 23),
                        IsAvailable = true
                    };

                    #endregion

                    #region Authors Information
                    // Create author objects with their first and last names
                    var author1 = new Author
                    {
                        FirstName = "Mary",
                        LastName = "Shelley",
                    };

                    var author2 = new Author
                    {
                        FirstName = "Mark",
                        LastName = "Twain",
                    };

                    var author3 = new Author
                    {
                        FirstName = "Ernest",
                        LastName = "Hemingway",
                    };

                    var author4 = new Author
                    {
                        FirstName = "Jane",
                        LastName = "Austen",
                    };

                    var author5 = new Author
                    {
                        FirstName = "J.K.",
                        LastName = "Rowling",
                    };

                    var author6 = new Author
                    {
                        FirstName = "J.R.R.",
                        LastName = "Tolkien",
                    };
                    var author7 = new Author
                    {
                        FirstName = "Agatha",
                        LastName = "Christie",
                    };

                    var author8 = new Author
                    {
                        FirstName = "Virginia",
                        LastName = "Woolf",
                    };

                    var author9 = new Author
                    {
                        FirstName = "Leo",
                        LastName = "Tolstoy",
                    };
                    #endregion

                    #region Adding books and Authors    // Add all book objects to the context

                    context.Books.Add(book1);
                    context.Books.Add(book2);
                    context.Books.Add(book3);
                    context.Books.Add(book4);
                    context.Books.Add(book5);
                    context.Books.Add(book6);
                    context.Books.Add(book7);
                    context.Books.Add(book8);
                    context.Books.Add(book9);

                    context.Authors.Add(author1);
                    context.Authors.Add(author2);
                    context.Authors.Add(author3);
                    context.Authors.Add(author4);
                    context.Authors.Add(author5);
                    context.Authors.Add(author6);
                    context.Authors.Add(author7);
                    context.Authors.Add(author8);
                    context.Authors.Add(author9);

                    // extra error handling to avoid error in database
                    try
                    {
                        // Save the added books and authors to the database
                        context.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Database update failed: {ex.Message}");
                    }
                    #endregion

                    #region Adding Book-Author Relationships
                    // Create relationships between books and authors
                    var bookAuthors = new[]
                    {
                            new BookAuthor {Book = book1, Author = author1},
                            new BookAuthor {Book = book2, Author = author2},
                            new BookAuthor {Book = book3, Author = author3},
                            new BookAuthor {Book = book4, Author = author4},
                            new BookAuthor {Book = book5, Author = author5},
                            new BookAuthor {Book = book6, Author = author6},
                            new BookAuthor {Book = book7, Author = author7},
                            new BookAuthor {Book = book8, Author = author8},
                            new BookAuthor {Book = book9, Author = author9},
                        };

                    // Ensure relationships are unique before adding
                    foreach (var ba in bookAuthors)
                    {
                        if (!context.BookAuthors.Any(existing =>
                        existing.BookID == ba.Book.BookId && existing.AuthorID == ba.Author.AuthorId))
                        {
                            context.BookAuthors.Add(ba);
                        }
                    }


                    // Relationships in this context will be added.
                    context.BookAuthors.AddRange(bookAuthors);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Database update failed: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    #endregion


                    // Commit the transaction to finalize all changes 
                    transaction.Commit();
                    System.Console.WriteLine("Saved changes");
                }
                else
                {
                    // Inform the user if the data is already seeded
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Already inserted.");
                    Console.ResetColor();
                }
            }

            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                transaction.Rollback();
                System.Console.WriteLine("Pardon our dust! An error occurred: " + ex.Message);
                System.Console.WriteLine("Transaction was rolled back!" + ex.Message);
            }
        }
    }
}