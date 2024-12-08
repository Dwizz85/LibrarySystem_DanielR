using System;
using LibrarySystem_DanielR;
using System.Linq;

namespace LibrarySystem_DanielR
{
    public class MainMenuIndex
    {
        public void MainMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear(); // Clear the screen for readability
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("          Welcome to DA Library!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  1. Loan Book");
                Console.WriteLine("  2. Return Loans");
                Console.WriteLine("  3. Library Listings");
                Console.WriteLine("  4. Member Administration");
                Console.WriteLine("  5. Library Administration\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  6. Exit Program");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Enter your choice: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int menuSel))
                {
                    switch (menuSel)
                    {
                        case 1:
                            LoanBook.Run();
                            break;
                        case 2:
                            ReturnBook.Run();
                            break;
                        case 3:
                            ListingIndex.Run();
                            break;
                        case 4:
                            MemberAdminMenu.Run();
                            break;
                        case 5:
                            LibraryAdmin.Run();
                            break;
                        case 6:
                            // Console.ForegroundColor = ConsoleColor.Yellow;
                            // Console.WriteLine("\nExiting Program...\n");
                            // Console.ResetColor();
                            isRunning = false; // Exit the main menu loop
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice. Please select a valid menu option.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                }

                // Pause before redisplaying the main menu
                // if (isRunning)
                // {
                //     Console.ForegroundColor = ConsoleColor.Blue;
                //     Console.WriteLine("\nPress any key to return to the main menu...");
                //     Console.ResetColor();
                //     Console.ReadKey();
                // }
            }
        }
    }
}