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
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("\nBooks available for Loan:\n");
            Console.ResetColor();
            var Books = context.Books
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .ToList();
        
            if (!Books.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\nNo books available for loan at this moment!\n");
                Console.ResetColor();
                return;
            }

            foreach (var _book in Books)
            {
                var authors = string.Join(",", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
                Console.WriteLine($"Book ID: {_book.BookId,-10} Title: {_book.Title,-40} Authors: {authors}");   
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\nEnter the Book ID to loan:\n");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please enter a valid numeric Book ID.\n");
                Console.ResetColor();
                return;
            }

            var selectedBook = context.Books.FirstOrDefault(b => b.BookId == bookId && b.IsAvailable);

            if (selectedBook == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBook ID not found or the book is not available.\n");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\nEnter Member ID to associate with the loan:\n");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int memberId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please enter a valid numeric Member ID.\n");
                Console.ResetColor();
                return;
            }

            var member = context.Members.FirstOrDefault(m => m.MemberID == memberId);

            if (member == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nMember ID not found.\n");
                Console.ResetColor();
                return;
            }

            // Confirm loan creation
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nConfirm loaning '{selectedBook.Title}' to {member.FirstName} {member.LastName}? (y/n):\n");
            Console.ResetColor();

            var confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation != "y")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nLoan canceled.\n");
                Console.ResetColor();
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
            Console.WriteLine($"\nBook '{selectedBook.Title}' has been loaned to {member.FirstName} {member.LastName}!\n");
            Console.ResetColor();
        }
    }
}

public class ReturnBook
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {       
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
                    Console.WriteLine("\nNo books are currently loaned out.\n");
                    Console.ResetColor();
                    return;
                }

                foreach (var loan in loanedBooks)
                {
                    Console.WriteLine($"Loan ID: {loan.LoanID,-10} Book: {loan.Title,-30} Member: {loan.MemberName,-20} Loan Date: {loan.LoanDate:yyyy-MM-dd}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter the Loan ID to return the book:\n");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out int loanId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a valid numeric Loan ID.\n");
                    Console.ResetColor();
                    return;
                }

                var loanToReturn = context.Loans.Include(l => l.Book).FirstOrDefault(l => l.LoanID == loanId && !l.IsReturned);
                
                if (loanToReturn == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLoan ID not found or the book has already been returned.\n");
                    Console.ResetColor();
                    return;
                }

                // Confirm return
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nConfirm returning the book '{loanToReturn.Book.Title}'? (y/n):\n");
                Console.ResetColor();

                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nReturn canceled.\n");
                    Console.ResetColor();
                    return;
                }

                // Mark the loan as returned
                loanToReturn.IsReturned = true;
                loanToReturn.ReturnDate = DateTime.Now;

                // Update book availability
                loanToReturn.Book.IsAvailable = true;

                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nBook '{loanToReturn.Book.Title}' has been returned successfully!\n");
                Console.ResetColor();
        }
    }
}