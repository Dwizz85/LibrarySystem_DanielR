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
                Console.WriteLine("  5. List specific Author Books.\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  6. Return to Main Menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Enter your choice: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 6)
                {
                    isRunning = HandleInput(menuSel);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a number between 1 and 6.\n");
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
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nReturning to Main Menu...");
                    Console.ResetColor();
                    return false;               // Exit the ListingMenu loop
                default:
                Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ResetColor();
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to continue...\n");
            Console.ResetColor();
            Console.ReadKey();
            return true; // Keep the loop running
        }
    }
}
