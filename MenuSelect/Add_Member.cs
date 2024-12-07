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
                Console.WriteLine("Adding New Member:");
                Console.WriteLine("__________________\n");

                var _firstName = GetInput("1: Enter First Name:");
                var _lastName = GetInput("2: Enter Last Name:");

                if (string.IsNullOrWhiteSpace(_firstName) || string.IsNullOrWhiteSpace(_lastName))
                {
                    Console.WriteLine("First and Last name must be entered!");
                    continue;
                }

                // Check if member already exists
                var _existingMember = context.Members
                    .FirstOrDefault(m => m.FirstName == _firstName && m.LastName == _lastName);

                if (_existingMember != null)
                {
                    Console.WriteLine($"Member '{_firstName} {_lastName}' already exists in the database.");
                    Console.WriteLine("Returning to menu.");
                    return; // Exit if the member already exists
                }

                var _email = GetInput("3: Enter Email:");

                if (string.IsNullOrWhiteSpace(_email))
                {
                    Console.WriteLine("Please enter an Email address!");
                    continue;
                }

                // Confirm details before saving
                Console.WriteLine($"Confirm adding the member:\nFirst Name: {_firstName}\nLast Name: {_lastName}\nEmail: {_email}\n(y/n):");
                var confirmation = Console.ReadLine()?.Trim().ToLower();
                if (confirmation != "y")
                {
                    Console.WriteLine("Action canceled.");
                    continue;
                }

                // Create and save the new member
                var _member = new Member
                {
                    FirstName = _firstName,
                    LastName = _lastName,
                    Email = _email
                };

                context.Members.Add(_member);
                context.SaveChanges();
                Console.WriteLine($"Member '{_firstName} {_lastName}' with Email '{_email}' has been registered!");
                Console.WriteLine("And so it begins... The time of book loaning is here!");

                isRunning = false; // Exit the loop
            }
                string GetInput(string message)
                {
                    Console.WriteLine(message);
                    return Console.ReadLine()?.Trim();
                }
        }
    }
}