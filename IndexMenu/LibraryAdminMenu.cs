using System;
using LibrarySystem_DanielR;
using System.Linq;

namespace LibrarySystem_DanielR
{
    public class LibraryAdmin
    {
        public void LibraryAdminMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear(); // Clear the screen for readability
                Console.WriteLine("========================================");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("         Library Administration");
                Console.ResetColor();
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("  1. Register new Book");
                Console.WriteLine("  2. Register new Author");
                Console.WriteLine("  3. Add relations to Author => Book");
                Console.WriteLine("  4. Update Book info");
                Console.WriteLine("  5. Update Author");
                Console.WriteLine("  6. Remove Author");
                Console.WriteLine("  7. Remove Book\n");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("  8. Return to Main Menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Enter your choice: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 8)
                {
                    isRunning = HandleInput(menuSel);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 8.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        public bool HandleInput(int menuSel)
        {
            switch (menuSel)
            {
                case 1:
                    Console.WriteLine("Registering a new Book...");
                    // Call method to register a new book
                    break;
                case 2:
                    Console.WriteLine("Registering a new Author...");
                    // Call method to register a new author
                    break;
                case 3:
                    Console.WriteLine("Adding relations to Author => Book...");
                    // Call method to add author-book relations
                    break;
                case 4:
                    Console.WriteLine("Updating Book info...");
                    // Call method to update book info
                    break;
                case 5:
                    Console.WriteLine("Updating Author...");
                    // Call method to update author info
                    break;
                case 6:
                    Console.WriteLine("Removing an Author...");
                    // Call method to remove an author
                    break;
                case 7:
                    Console.WriteLine("Removing a Book...");
                    // Call method to remove a book
                    break;
                case 8:
                    Console.WriteLine("\nReturning to Main Menu...");
                    return false; // Exit the loop and return to Main Menu
                default:
                    Console.WriteLine("Invalid choice, please try again!");
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
