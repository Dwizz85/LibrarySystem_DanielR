using System;
using LibrarySystem_DanielR;
using System.Linq;

namespace LibrarySystem_DanielR
{
    public class RemoveMenuIndex
    {
        public void RemoveMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear(); // Clear the console for better readability
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("      Remove from Library: Author/Book");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  1. Remove Author");
                Console.WriteLine("\n  2. Remove Book");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("  3. Return to Main Menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 3)
                {
                    isRunning = HandleInput(menuSel);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.\n");
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
                    RemoveAuthor.Run();
                    break;
                case 2:
                    RemoveBook.Run();
                    break;
                case 3:
                    Console.WriteLine("Returning to Main Menu...");
                    return false; // Exit the RemoveMenu loop
                default:
                    Console.WriteLine("Invalid choice, please try again!");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
            return true; // Keep the loop running
        }
    }
}
