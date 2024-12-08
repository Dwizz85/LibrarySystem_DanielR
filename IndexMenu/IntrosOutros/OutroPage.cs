namespace LibrarySystem_DanielR
{
    public class OutroPage
    {
        public void ShowOutro()
        {
            Console.Clear();

            // Display the thank-you message in a fixed area
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("***************************************************");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("*      Thank you for reviewing our project!       *");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("*                Project by Daniel                *");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("*                 To: Aladdin :)                  *");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("*     We hope you enjoyed exploring the library!  *");
            Console.WriteLine("***************************************************");
            Console.ResetColor();

            // Fireworks display
            Console.WriteLine("\nStarting Fireworks!\n");
            ShowFireworks();

            // Final message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***************************************************");
            Console.WriteLine("*      Thank you, and have a wonderful day!       *");
            Console.WriteLine("***************************************************");
            Console.ResetColor();

            // Pause before exiting
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to exit...");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void ShowFireworks()
        {
            var random = new Random();

            for (int i = 0; i < 10; i++) // Adjust the number of bursts
            {
                Console.SetCursorPosition(0, 15); // Start fireworks lower down
                for (int j = 0; j < 5; j++) // Fireworks burst effect
                {
                    int fireworkX = random.Next(0, Console.WindowWidth - 10);
                    int fireworkY = random.Next(10, 20); // Fireworks happen lower on the screen
                    Console.SetCursorPosition(fireworkX, fireworkY);

                    Console.ForegroundColor = (ConsoleColor)random.Next(1, 16);
                    Console.WriteLine("*");
                }
                System.Threading.Thread.Sleep(300);
            }
        }
    }
}
