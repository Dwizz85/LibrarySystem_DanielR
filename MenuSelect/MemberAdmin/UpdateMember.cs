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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("           Update Member Details");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("  1. View and Update First Name");
            Console.WriteLine("  2. View and Update Last Name");
            Console.WriteLine("  3. View and Update Email");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n  4. Cancel and Return to Main Menu");
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
                Console.WriteLine("\nInvalid input. Please enter a number between 1 and 4.\n");
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
        using (var context = new AppDbContext())
        {
            try
            {
                if (menuSel == 4) // Handle the "Cancel" option here
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
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

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Listing Members:\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ID       Name                           Email");
                Console.WriteLine("------------------------------------------------------");
                Console.ResetColor();

                    foreach (var member in members)
                    {
                        Console.WriteLine($"{member.MemberID,-8} {member.FirstName,-15} {member.LastName,-15} {member.Email}");
                    }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nEnter the Member ID to update (or press Enter to return)");
                Console.ResetColor();
                var input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nReturning to menu...");
                        Console.ResetColor();
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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
            return true; // Keep the loop running
        }
    }

    private static void UpdateFirstName(Member selectedMember) // update member - First name
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\nEnter new First Name:");
        Console.ResetColor();

        // read user input - error handling
        var newFirstName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newFirstName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. First Name not updated.");
                Console.ResetColor();
            }
            else
            {
                selectedMember.FirstName = newFirstName;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nFirst Name updated to {newFirstName}.");
                Console.ResetColor();
            }
    }

    private static void UpdateLastName(Member selectedMember)   // update member - Last name
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\nEnter new Last Name:");
        Console.ResetColor();

        // user input - error handling
        var newLastName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newLastName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Last Name not updated.");
                Console.ResetColor();
            }
            else
            {
                selectedMember.LastName = newLastName;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nLast Name updated to {newLastName}.");
                Console.ResetColor();
            }
    }

    private static void UpdateEmail(Member selectedMember)  // update member - email
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\nEnter new Email:");
        Console.ResetColor();

        // user input - error handling
        var newEmail = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid email format. Email not updated.");
                Console.ResetColor();
            }
            else
            {
                selectedMember.Email = newEmail;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nEmail updated to {newEmail}.");
                Console.ResetColor();
            }
    }
}
