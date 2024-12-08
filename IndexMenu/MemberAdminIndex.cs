using System;
using LibrarySystem_DanielR;
using System.Linq;

namespace LibrarySystem_DanielR
{
    public class MemberAdminMenu
    {
        public void MemberAdmin()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear(); // Clear the console for readability
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("       Member Administration Menu");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  1. Register Member");
                Console.WriteLine("  2. Update Member");
                Console.WriteLine("  3. Remove Member\n");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("  4. Return to Main Menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Enter your choice: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 4)
                {
                    isRunning = HandleInput(menuSel);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nPress any key to try again.");
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
                    AddMember.Run();
                    break;
                case 2:
                    UpdateMember.Run();
                    break;
                case 3:
                    RemoveMember.Run();
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nReturning to Main Menu...");
                    Console.ResetColor();
                    return false; // Exit the MemberAdmin loop
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPress any key to continue...\n");
            Console.ResetColor();
            Console.ReadKey();
            return true; // Keep the loop running
        }
    }
}
