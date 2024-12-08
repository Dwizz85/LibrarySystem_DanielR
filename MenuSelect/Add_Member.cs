using LibrarySystem_DanielR;

public class AddMember
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear(); // Clear the console for better readability

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nAdding New Member:\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                var _firstName = GetInput("\n1: Enter First Name (or press Enter to cancel):");

                if (string.IsNullOrWhiteSpace(_firstName))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Press any key to continue.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit the function if canceled
                }

                var _lastName = GetInput("\n2: Enter Last Name (or press Enter to cancel):");
                if (string.IsNullOrWhiteSpace(_lastName))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Press any key to continue.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit the function if canceled
                }
                Console.ResetColor();

                // Check if member already exists
                var _existingMember = context.Members
                    .FirstOrDefault(m => m.FirstName == _firstName && m.LastName == _lastName);

                if (_existingMember != null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nMember '{_firstName} {_lastName}' already exists in the database.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nReturning to menu. Press any key to continue.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit if the member already exists
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                var _email = GetInput("\n3: Enter Email (or press Enter to cancel):");
                if (string.IsNullOrWhiteSpace(_email))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Press any key to continue.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; // Exit the function if canceled
                }
                Console.ResetColor();

                // Confirm details before saving
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n\nConfirm adding the member:\n\nFirst Name: {_firstName}\n\nLast Name: {_lastName}\n\nEmail: {_email}\n\n(y/n):");
                Console.ResetColor();
                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Press any key to try again.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue; // Retry
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
                Console.WriteLine($"\nMember '{_firstName} {_lastName}' with Email '{_email}' has been registered!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nAnd so it begins... The time of book loaning is here!\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ResetColor();
                Console.ReadKey();
                isRunning = false; // Exit the loop after successful addition
            }

            // Nested helper function for input
            string GetInput(string message)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(message);
                Console.ResetColor();
                return Console.ReadLine()?.Trim();
            }
        }
    }
}
