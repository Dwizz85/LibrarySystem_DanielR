using System;
using LibrarySystem_DanielR;


namespace LibrarySystem_DanielR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Run seeding function to populate data
            try
            {
                Seed.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error during seeding: {ex.Message}");
                Console.ResetColor();
                return; // Exit the program if seeding fails
            }

            // Instantiate MainMenuIndex
            var mainMenu = new MainMenuIndex();
            
            // Instantiate IntroPage
            var introPage = new IntroPage();
            introPage.ShowIntro();

            // Run the Main Menu
            mainMenu.MainMenu();
        }
    }
}