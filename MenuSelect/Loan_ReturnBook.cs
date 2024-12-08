using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore;


public class LoanBook   // Class to loan => book => relate => member
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true) // Main loop to allow retrying
            {
                Console.Clear(); // Clear the console for better readability

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nBooks available for Loan:\n");
                Console.ResetColor();

                var Books = context.Books
                    .Include(ba => ba.BookAuthors)
                    .ThenInclude(a => a.Author)
                    .ToList();

                if (!Books.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo books available for loan at this moment!");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                foreach (var _book in Books)
                {
                    var authors = string.Join(",", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
                    Console.WriteLine($"Book ID: {_book.BookId,-10} Title: {_book.Title,-40} Authors: {authors}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the Book ID to loan (or press Enter to cancel): ");
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

                if (!int.TryParse(bookInput, out int bookId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a valid numeric Book ID.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                var selectedBook = context.Books.FirstOrDefault(b => b.BookId == bookId && b.IsAvailable);

                if (selectedBook == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBook ID not found or the book is not available.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                Console.Clear();
                var Members = context.Members.ToList();

                if (!Members.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo members found in the system.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nListing All Members:\n");
                Console.ResetColor();

                foreach (var item in Members)
                {
                    Console.WriteLine($"Member ID: {item.MemberID,-10} Name: {item.FirstName} {item.LastName}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter Member ID to associate with the loan (or press Enter to cancel): ");
                Console.ResetColor();

                var memberInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(memberInput))
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

                if (!int.TryParse(memberInput, out int memberId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a valid Member ID.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                var member = context.Members.FirstOrDefault(m => m.MemberID == memberId);

                if (member == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nMember ID not found.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                // Confirm loan creation
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nConfirm loaning '{selectedBook.Title}' to {member.FirstName} {member.LastName}? (y/n):");
                Console.ResetColor();

                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLoan canceled.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                // Create the loan
                var loan = new Loan
                {
                    BookID = selectedBook.BookId,
                    MemberID = member.MemberID,
                    LoanDate = DateTime.Now,
                    IsReturned = false
                };

                context.Loans.Add(loan);

                // Update book availability
                selectedBook.IsAvailable = false;

                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nBook '{selectedBook.Title}' has been loaned to {member.FirstName} {member.LastName}!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return...");
                Console.ResetColor();
                Console.ReadKey();
                return; // Exit after successful loan
            }
        }
    }
}


public class ReturnBook
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true) // Main loop for retrying
            {

                Console.Clear(); // Clear the console for better readability


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nBooks currently loaned out:\n");
                Console.ResetColor();


                var loanedBooks = context.Loans
                    .Include(l => l.Book)
                    .Include(l => l.Member)
                    .Where(l => !l.IsReturned) // Only fetch active loans
                    .Select(l => new
                    {
                        l.LoanID,
                        l.Book.Title,
                        MemberName = $"{l.Member.FirstName} {l.Member.LastName}",
                        l.LoanDate
                    })
                    .ToList();


                if (!loanedBooks.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo books are currently loaned out.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit the function if no loans exist
                }

                // Display active loans
                foreach (var loan in loanedBooks)
                {
                    Console.WriteLine($"Loan ID: {loan.LoanID,-10} Book: {loan.Title,-30} Member: {loan.MemberName,-20} Loan Date: {loan.LoanDate:yyyy-MM-dd}");
                }


                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the Loan ID to return the book (or press Enter to cancel): ");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Returning to menu...");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit the loop if canceled
                }

                if (!int.TryParse(input, out int loanId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a valid numeric Loan ID.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }


                var loanToReturn = context.Loans
                    .Include(l => l.Book)
                    .FirstOrDefault(l => l.LoanID == loanId && !l.IsReturned);


                if (loanToReturn == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLoan ID not found or the book has already been returned.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry the loop
                }

                // Confirm return
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nConfirm returning the book '{loanToReturn.Book.Title}'? (y/n):");
                Console.ResetColor();

                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nReturn canceled.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPress any key to return...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit the loop if canceled
                }

                // Mark the loan as returned
                loanToReturn.IsReturned = true;
                loanToReturn.ReturnDate = DateTime.Now;

                // Update book availability
                loanToReturn.Book.IsAvailable = true;

                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nBook '{loanToReturn.Book.Title}' has been returned successfully!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return to the menu...\n");
                Console.ResetColor();
                Console.ReadKey();
                return; // Exit after successful return
            }
        }
    }
}
