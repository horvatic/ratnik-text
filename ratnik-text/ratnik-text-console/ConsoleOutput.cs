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

        public void Print((int x, int y) pos, IFileBuffer fileBuffer)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(fileBuffer.Read((pos.x, pos.y), _consoleScreen.GetPage()));
            var (x, y) = _consoleScreen.GetCursorPosition();
            Console.SetCursorPosition(x, y);
        }

        public void PrintOnNewPage(int page, IFileBuffer fileBuffer)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            var buffer = fileBuffer.ReadPage(page);
            for (var y = 0; y < Console.WindowHeight; y++)
            {
                for (var x = 0; x < Console.WindowWidth; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(buffer[x + (y * Console.WindowWidth)]);
                }
            }
            Console.SetCursorPosition(0, 0);
            _consoleScreen.SetCursorPositionOnNewPage(page);
        }
    }
}
