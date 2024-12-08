using System;
using System.Linq;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore;

public class RemoveMember
{
    public static void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("           Remove Member");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("  1. View and Remove Member");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  2. Cancel and Return to Main Menu");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Enter your choice: ");
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 2)
            {
                isRunning = HandleInput(menuSel);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please enter a number between 1 and 2.\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press any key to try again...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }

    private static bool HandleInput(int menuSel)
    {
        switch (menuSel)
        {
            case 1:
                RemoveMemberDetails();
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nReturning to Main Menu...");
                Console.ResetColor();
                return false; // Exit the loop
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid choice. Returning to menu...");
                Console.ResetColor();
                break;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nPress any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
        return true; // Keep the loop running
    }

    private static void RemoveMemberDetails()
    {
        using (var context = new AppDbContext())
        {
            try
            {
                // Fetch all members from the database
                var members = context.Members.ToList();

                if (!members.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo members found in the database.");
                    Console.ResetColor();
                    return;
                }

                // Display the list of members (no console clear here)
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nListing Members:\n");
                Console.ResetColor();

                foreach (var member in members)
                {
                    Console.WriteLine($"Member ID: {member.MemberID,-10} Name: {member.FirstName} {member.LastName,-20} Email: {member.Email}");
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nEnter the Member ID to remove or press Enter to return:");
                Console.ResetColor();
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nReturning to menu...");
                    Console.ResetColor();
                    return;
                }

                if (!int.TryParse(input, out int memberId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Member ID. Returning to menu...");
                    Console.ResetColor();
                    return;
                }

                // Fetch the member by ID
                var selectedMember = context.Members.FirstOrDefault(m => m.MemberID == memberId);
                if (selectedMember == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nNo member found with ID {memberId}. Returning to menu...");
                    Console.ResetColor();
                    return;
                }

                // Confirm deletion
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAre you sure you want to remove member: {selectedMember.FirstName} {selectedMember.LastName} (ID: {selectedMember.MemberID})? (y/n)");
                Console.ResetColor();
                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation == "y")
                {
                    context.Members.Remove(selectedMember);
                    context.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\nMember '{selectedMember.FirstName} {selectedMember.LastName}' has been successfully removed.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Member was not removed.");
                    Console.ResetColor();
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nDatabase update failed: {dbEx.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
