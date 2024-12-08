using System;
using LibrarySystem_DanielR;
using System.Linq;

namespace LibrarySystem_DanielR
{
    public class ListingIndex
    {
        public static void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear(); // Clear the screen for readability
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("          Library Listing Menu");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  1. List Loans");
                Console.WriteLine("  2. List Loan History");
                Console.WriteLine("  3. List Registered Books");
                Console.WriteLine("  4. List Authors with Books");
                Console.WriteLine("  5. List specific Author with Books.");
                Console.WriteLine("  6. List specific Books with Authors.\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  7. Return to Main Menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Enter your choice: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 7)
                {
                    isRunning = HandleInput(menuSel);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a number between 1 and 7.\n");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Press any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

        public static bool HandleInput(int menuSel)
        {
            switch (menuSel)
            {
                case 1:
                    ActiveLoans.Run();          // lists active loans
                    break;
                case 2:
                    LoanHistory.Run();          // Runs Loan history
                    break;
                case 3:
                    BooksOnlyListing.Run();     // runs listing only books
                    break;
                case 4:
                    ListLibrary.Run();          // list authors with books
                    break;
                case 5:
                    BooksByAuthorListing.Run();     // Books listed by Authors
                    break;
                case 6:
                    AuthorsByBookListing.Run();
                    break;
                case 7:
                    return false;               // Exit the ListingMenu loop
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    Console.ResetColor();
                    break;
            }

            return true; // Keep the loop running
        }
    }
}
