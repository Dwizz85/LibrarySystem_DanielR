using System;
using LibrarySystem_DanielR;

namespace LibrarySystem_DanielR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            try     // Run seeding function to populate data
            {
                Seed.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
                return; // Exit the program if seeding fails
            }

            // Instantiate MainMenuIndex
            
            
            // // Instantiate IntroPage and Run
            // var introPage = new IntroPage();
            // introPage.ShowIntro();

            // Run the Main Menu
            var mainMenuIndex = new MainMenuIndex(); // Create an instance
            mainMenuIndex.MainMenu(); // Call the instance method

            // // Instantiate OutroPage and Run
            // var outroPage = new OutroPage();
            // outroPage.ShowOutro();
        }
    }
}