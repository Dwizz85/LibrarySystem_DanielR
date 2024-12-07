using System;
using LibrarySystem_DanielR;
using System.Linq;

namespace LibrarySystem_DanielR
{
    public class IntroPage()
    {
        public void ShowIntro()
        {
            Console.Clear();
            
            string introText = "\n\tWelcome to DA Library System!\n";
            string tagline = "\n\tBringing knowledge to your fingertips.\n";
            string presented = "\n\n\tCreated by: Daniel Rutks SYSS7";

            // Display the main intro text letter by letter
            foreach (char c in introText)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(c);
                Console.ResetColor();
                Thread.Sleep(100);          // Adjust the delay for speed (in milliseconds)
            }

            Thread.Sleep(500);              // Pause before displaying the tagline
            Console.WriteLine();            // Move to the next line

            foreach (char c in tagline)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(c);
                Console.ResetColor();
                Thread.Sleep(75);
            }

            foreach (char c in presented)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(c);
                Console.ResetColor();
                Thread.Sleep(75);
            }

            // Pause before starting the loading animation
            Thread.Sleep(1000);
            ShowLoading();
        }
        public static void ShowLoading()
        {
            Console.Clear();

            // Calculate positioning to align with the intro text layout
            int spinnerRow = 8; // A few lines down for alignment with the intro text
            int spinnerCol = 15; // Indent similar to "\t" spacing

            char[] spinner = { '|', '/', '-', '\\' };
            for (int i = 0; i < 20; i++) // Loop for the spinner animation
            {
                Console.SetCursorPosition(spinnerCol, spinnerRow); // Set cursor position
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"Loading {spinner[i % spinner.Length]}"); // Spinner text
                Console.ResetColor();
                Thread.Sleep(200);
            }

            Thread.Sleep(1000); // Final pause before clearing the console
            Console.Clear();
        }
    
    }
}
