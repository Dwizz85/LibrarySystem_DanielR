using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class AddMember
{

    public static void Run()
    {

        using (var context = new AppDbContext())
        {
            bool isRunning = true;

            while (isRunning)
            {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nAdding New Member:");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    var _firstName = GetInput("\n1: Enter First Name:");
                    var _lastName = GetInput("\n2: Enter Last Name:");
                    Console.ResetColor();

                    if (string.IsNullOrWhiteSpace(_firstName) || string.IsNullOrWhiteSpace(_lastName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nFirst and Last name must be entered!\n");
                        Console.ResetColor();
                        continue;
                    }

                    // Check if member already exists
                    var _existingMember = context.Members
                        .FirstOrDefault(m => m.FirstName == _firstName && m.LastName == _lastName);

                    if (_existingMember != null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nMember '{_firstName} {_lastName}' already exists in the database.");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\nReturning to menu.\n");
                        Console.ResetColor();
                        return; // Exit if the member already exists
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    var _email = GetInput("\n3: Enter Email:");
                    Console.ResetColor();

                    if (string.IsNullOrWhiteSpace(_email))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter an Email address!");
                        Console.ResetColor();
                        continue;
                    }

                    // Confirm details before saving
                    Console.WriteLine($"\nConfirm adding the member:\nFirst Name: {_firstName}\nLast Name: {_lastName}\nEmail: {_email}\n(y/n):");
                    var confirmation = Console.ReadLine()?.Trim().ToLower();
                    if (confirmation != "y")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nAction canceled.\n");
                        Console.ResetColor();
                        continue;
                    }

                    // Create and save the new member
                    var _member = new Member
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        Email = _email
                    };

                    Console.Clear();
                    context.Members.Add(_member);
                    context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\nMember '{_firstName} {_lastName}' with Email '{_email}' has been registered!\n");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("And so it begins... The time of book loaning is here!\n");
                    Console.ResetColor();

                    isRunning = false; // Exit the loop
                }
                    string GetInput(string message)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(message);
                        Console.ResetColor();
                        return Console.ReadLine()?.Trim();
                    }
        }
    }
}