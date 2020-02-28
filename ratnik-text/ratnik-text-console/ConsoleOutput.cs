using System;

namespace ratnik_text_console
{
    public class ConsoleOutput : IConsoleOutput
    {
        private readonly IConsoleScreen _consoleScreen;

        public ConsoleOutput(IConsoleScreen consoleScreen)
        {
            _consoleScreen = consoleScreen;
        }

        public void Print(IFileBuffer fileBuffer)
        {
            var (x, y) = _consoleScreen.GetCursorPosition();
            Console.SetCursorPosition(x, y);
            Console.Write(fileBuffer.Read((x, y), _consoleScreen.GetPage()));
        }

        public void PrintOnNewPage(int page, IFileBuffer fileBuffer)
        {
            Console.SetCursorPosition(0, 0);
            for (var x = 0; x < System.Console.WindowWidth; x++)
            {
                for (var y = 0; y < System.Console.WindowHeight; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(fileBuffer.Read((x, y), page));
                }
            }
            Console.SetCursorPosition(0, 0);
            _consoleScreen.SetCursorPositionOnNewPage(page);
        }
    }
}
