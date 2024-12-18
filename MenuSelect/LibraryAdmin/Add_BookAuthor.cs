using LibrarySystem_DanielR;

public class AddBook // Class => Add book => Create => CRUD
{
    public static void Run()
    {
        bool isRunning = true; // Loop control variable

        while (isRunning)
        {
            using (var context = new AppDbContext())
            {
                Console.Clear(); // Clear the console for better readability

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nAdd new Book to Library.\n");
                Console.ResetColor();

                // ask user for Title/Book input
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Enter Title (or press Enter to cancel): ");
                Console.ResetColor();
                
                // variable to read user input => title
                var _title = Console.ReadLine()?.Trim();

                // error handling - breaks loop => empty/null
                if (string.IsNullOrEmpty(_title))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nOperation canceled. Returning to menu...");
                    Console.ResetColor();
                    break; // Exit the loop
                }
                
                // variable Year Published
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nEnter Year Published (yyyy):");
                Console.ResetColor();

                // variable to read user input => Year published
                var _yearPublished = Console.ReadLine()?.Trim();

                // error handling - restart loop => inccorect format
                if (!int.TryParse(_yearPublished, out int YearPublished))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nFormat incorrect. Please enter the year in yyyy format.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to try again...");
                    Console.ReadKey();
                    continue; // Retry from the start of the loop
                }

                // confirm => add => New Book
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nConfirm adding the book '{_title}' published in {YearPublished} (y/n):");
                Console.ResetColor();

                // Confirmation or Exit loop => new entry Book
                var confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAction canceled. Returning to menu...");
                    Console.ResetColor();
                    break; // Exit the loop
                }

                // Add the book to the database
                var _book = new Book
                {
                    Title = _title,
                    YearPublished = YearPublished,
                    IsAvailable = true
                };

                context.Books.Add(_book);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nThe Book '{_title}' has been successfully added to the library!");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to continue...");
                Console.ResetColor();
                Console.ReadKey();
                isRunning = false; // Exit the loop after successful addition
            }
        }
    }
}
public class AddAuthor
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            bool isRunning = true;

                while (isRunning)
                {
                    Console.Clear(); // Clear the console for readability

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nAdd new Author to library:\n");
                    Console.ResetColor();

                    var _firstName = GetInput("Enter First Name (or press Enter to cancel):");

                    if (string.IsNullOrWhiteSpace(_firstName))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nOperation canceled. Returning to menu...");
                        Console.ResetColor();
                        break; // Exit the loop if canceled
                    }

                    var _lastName = GetInput("Enter Last Name:");

                    if (string.IsNullOrWhiteSpace(_lastName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLast Name cannot be empty. To try again press any key.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue; // Retry the operation
                    }

                    // Check if the author already exists
                    if (context.Authors.Any(a => a.FirstName == _firstName && a.LastName == _lastName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nThis author already exists in the library.");
                        Console.ResetColor();
                        continue; // Retry the operation
                    }

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\nConfirm adding author\n\nFirst Name: '{_firstName}\n\nLast Name: {_lastName}'\n\n (y/n):");
                    Console.ResetColor();

                    var confirmation = Console.ReadLine()?.Trim().ToLower();

                    if (confirmation != "y")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nAction canceled.");
                        Console.ResetColor();
                        continue; // Retry or exit based on user input
                    }

                    // Add the new author to the database
                    var _author = new Author
                    {
                        FirstName = _firstName,
                        LastName = _lastName
                    };

                    
                    context.Authors.Add(_author);
                    context.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\nThe Author: '{_firstName} {_lastName}' has been added to the library!");
                    Console.ResetColor();

                    // Exit the loop after successful addition
                    isRunning = false; 
                }

                // Nested helper function to handle input
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



