using System;
using System.Threading;

namespace LibrarySystem_DanielR
{
    public class OutroPage
    {
        public void ShowOutro()
        {
            Console.Clear();

            string outroText = "\n\tThank You for Using DA Library System!\n";
            string tagline = "\n\tSee You Next Time!\n";
            string signature = "\n\n\tDeveloped with Passion by: Daniel Rutks SYSS7";

            // Display the outro text letter by letter
            foreach (char c in outroText)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(c);
                Console.ResetColor();
                Thread.Sleep(100); // Adjust the delay for speed (in milliseconds)
            }

            Thread.Sleep(500); // Pause before displaying the tagline
            Console.WriteLine(); // Move to the next line

            foreach (char c in tagline)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(c);
                Console.ResetColor();
                Thread.Sleep(75);
            }

            foreach (char c in signature)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(c);
                Console.ResetColor();
                Thread.Sleep(75);
            }

            // Pause before starting the closing animation
            Thread.Sleep(1000);
            ShowClosingAnimation();
        }

        public static void ShowClosingAnimation()
        {
            Console.Clear();

            // Create a shrinking rectangle animation
            int width = Console.WindowWidth - 4;
            int height = Console.WindowHeight - 4;
            int left = 2;
            int top = 2;

            while (width > 0 && height > 0)
            {
                Console.SetCursorPosition(left, top);
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Top border
                Console.Write(new string('═', width));
                for (int i = 1; i < height - 1; i++)
                {
                    // Left and right borders
                    Console.SetCursorPosition(left, top + i);
                    Console.Write("║");
                    Console.SetCursorPosition(left + width - 1, top + i);
                    Console.Write("║");
                }

                // Bottom border
                Console.SetCursorPosition(left, top + height - 1);
                Console.Write(new string('═', width));

                Console.ResetColor();

                // Shrink the rectangle
                left++;
                top++;
                width -= 2;
                height -= 2;

                Thread.Sleep(200);
            }

            // Final goodbye message
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("\n\n\tGoodbye and Have a Great Day!");
            Thread.Sleep(1500);
        }
    }
}
