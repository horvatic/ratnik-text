using System;

namespace ratnik_text_console
{
    public class ConsoleOutput : IConsoleOutput
    {
        private int lastPage;

        public ConsoleOutput()
        {
            lastPage = 0;
        }

        public void SetPage(int page)
        {
            lastPage = page;
        }

        public void PrintPage(int xCusorPos, int yCusorPos, int page, IFileBuffer fileBuffer)
        {
            if (lastPage != page)
            {
                Console.Clear();
            }
            lastPage = page;
            Console.SetCursorPosition(0, 0);
            var buffer = fileBuffer.ReadPage(page);
            Console.Write(buffer);
        }
    }
}
