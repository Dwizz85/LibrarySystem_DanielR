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
                Console.WriteLine($"Book ID: {_book.BookId,-10} Title: {_book.Title,-40} Authors: {authors}");   
            }

            Console.Write("\nEnter the Book ID to loan: ");

            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric Book ID.");
                return;
            }

            var selectedBook = context.Books.FirstOrDefault(b => b.BookId == bookId && b.IsAvailable);

            if (selectedBook == null)
            {
                Console.WriteLine("Book ID not found or the book is not available.");
                return;
            }

            Console.Write("Enter Member ID to associate with the loan: ");

            if (!int.TryParse(Console.ReadLine(), out int memberId))
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric Member ID.");
                return;
            }

            var member = context.Members.FirstOrDefault(m => m.MemberID == memberId);

            if (member == null)
            {
                Console.WriteLine("Member ID not found.");
                return;
            }

            // Confirm loan creation
            Console.WriteLine($"\nConfirm loaning '{selectedBook.Title}' to {member.FirstName} {member.LastName}? (y/n):");

            var confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation != "y")
            {
                Console.WriteLine("Loan canceled.");
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
            Console.WriteLine($"\nBook '{selectedBook.Title}' has been loaned to {member.FirstName} {member.LastName}!");
        }
    }
}

public class ReturnBook
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
                Console.WriteLine("Books currently loaned out:\n");

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
                    Console.WriteLine("No books are currently loaned out.");
                    return;
                }

                foreach (var loan in loanedBooks)
                {
                    Console.WriteLine($"Loan ID: {loan.LoanID,-10} Book: {loan.Title,-30} Member: {loan.MemberName,-20} Loan Date: {loan.LoanDate:yyyy-MM-dd}");
                }

                Console.Write("\nEnter the Loan ID to return the book: ");

                if (!int.TryParse(Console.ReadLine(), out int loanId))
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric Loan ID.");
                    return;
                }

                var loanToReturn = context.Loans.Include(l => l.Book).FirstOrDefault(l => l.LoanID == loanId && !l.IsReturned);
                
                if (loanToReturn == null)
                {
                    Console.WriteLine("Loan ID not found or the book has already been returned.");
                    return;
                }

                // Confirm return
                Console.WriteLine($"\nConfirm returning the book '{loanToReturn.Book.Title}'? (y/n):");

                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.WriteLine("Return canceled.");
                    return;
                }

                // Mark the loan as returned
                loanToReturn.IsReturned = true;
                loanToReturn.ReturnDate = DateTime.Now;

                // Update book availability
                loanToReturn.Book.IsAvailable = true;

                context.SaveChanges();
                Console.WriteLine($"\nBook '{loanToReturn.Book.Title}' has been returned successfully!");
        }
    }
}