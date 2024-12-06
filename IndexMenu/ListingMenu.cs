using System;
using LibrarySystem_DanielR;
using System.Linq;

namespace LibrarySystem_DanielR
{
    public class ListingIndex
    {
        public void ListingMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear(); // Clear the screen for readability
                Console.WriteLine("========================================");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("          Library Listing Menu");
                Console.ResetColor();
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("  1. List Loans");
                Console.WriteLine("  2. List Loan History");
                Console.WriteLine("  3. List Registered Books");
                Console.WriteLine("  4. List Authors with Books\n");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("  5. Return to Main Menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Enter your choice: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 5)
                {
                    isRunning = HandleInput(menuSel);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.\n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Press any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

        public bool HandleInput(int menuSel)
        {
            switch (menuSel)
            {
                case 1:
                    Console.WriteLine("Listing Loans...");
                    break;
                case 2:
                    Console.WriteLine("Listing Loan History...");
                    break;
                case 3:
                    Console.WriteLine("Listing Registered Books...");
                    break;
                case 4:
                    Console.WriteLine("Listing Authors with Books...");
                    break;
                case 5:
                    Console.WriteLine("\nReturning to Main Menu...");
                    return false; // Exit the ListingMenu loop
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
            return true; // Keep the loop running
        }
    }
}
