using System;

namespace ratnik_text_console
{
    public class ConsoleOutput : IConsoleOutput
    {
        public void Print((int xPos, int yPos) cusPos, int bufferPos, char[] buffer)
        {
            Console.SetCursorPosition(cusPos.xPos, cusPos.yPos);
            Console.Write(buffer[bufferPos]);
        }
    }
}
