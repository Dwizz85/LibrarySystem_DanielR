using System;
using System.Linq;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore;

public class UpdateMember
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("           Update Member Details");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("  1. View and Update First Name");
            Console.WriteLine("  2. View and Update Last Name");
            Console.WriteLine("  3. View and Update Email");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  4. Cancel and Return to Main Menu");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int menuSel) && menuSel >= 1 && menuSel <= 4)
            {
                isRunning = HandleInput(menuSel);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please enter a number between 1 and 4.\n");
                Console.ResetColor();
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }
        }
    }

    private static bool HandleInput(int menuSel)
    {
        using (var context = new AppDbContext())
        {
            try
            {
                if (menuSel == 4) // Handle the "Cancel" option here
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nReturning to Main Menu...");
                    Console.ResetColor();
                    return false; // Exit the loop
                }

                // Fetch all members from the database
                var members = context.Members.ToList();

                if (!members.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo members found in the database.");
                    Console.ResetColor();
                    return true; // Keep the loop running
                }

                // Display the list of members
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Listing Members:\n");
                Console.ResetColor();

                foreach (var member in members)
                {
                    Console.WriteLine($"Member ID: {member.MemberID,-10} Name: {member.FirstName} {member.LastName,-20} Email: {member.Email}");
                }

                Console.WriteLine("\nEnter the Member ID to update or press Enter to return:");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nReturning to menu...");
                    return true; // Keep the loop running
                }

                if (!int.TryParse(input, out int memberId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Member ID. Returning to menu...");
                    Console.ResetColor();
                    return true; // Keep the loop running
                }

                // Fetch the member by ID
                var selectedMember = context.Members.FirstOrDefault(m => m.MemberID == memberId);
                if (selectedMember == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nNo member found with ID {memberId}. Returning to menu...");
                    Console.ResetColor();
                    return true; // Keep the loop running
                }

                // Perform the appropriate update based on menu selection
                switch (menuSel)
                {
                    case 1:
                        UpdateFirstName(selectedMember);
                        break;
                    case 2:
                        UpdateLastName(selectedMember);
                        break;
                    case 3:
                        UpdateEmail(selectedMember);
                        break;
                }

                // Save changes only if any were made
                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nMember details updated successfully!");
                Console.ResetColor();
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

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
            return true; // Keep the loop running
        }
    }

    private static void UpdateFirstName(Member selectedMember)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\nEnter new First Name:");
        Console.ResetColor();
        var newFirstName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newFirstName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. First Name not updated.");
            Console.ResetColor();
        }
        else
        {
            selectedMember.FirstName = newFirstName;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"First Name updated to {newFirstName}.");
            Console.ResetColor();
        }
    }

    private static void UpdateLastName(Member selectedMember)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\nEnter new Last Name:");
        Console.ResetColor();
        var newLastName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newLastName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Last Name not updated.");
            Console.ResetColor();
        }
        else
        {
            selectedMember.LastName = newLastName;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Last Name updated to {newLastName}.");
            Console.ResetColor();
        }
    }

    private static void UpdateEmail(Member selectedMember)
    {
        Console.WriteLine("\nEnter new Email:");
        var newEmail = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid email format. Email not updated.");
            Console.ResetColor();
        }
        else
        {
            selectedMember.Email = newEmail;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Email updated to {newEmail}.");
            Console.ResetColor();
        }
    }
}
