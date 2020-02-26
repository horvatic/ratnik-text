using System;

namespace ratnik_text_console
{
    public class InputReader
    {
        public void Start()
        {
            var capacity = 1028;
            var buffer = new char[capacity];
            var pos = 0;
            while(true)
            {
                var c = Console.ReadKey(true);
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (pos != 0)
                    {
                        pos -= 1;
                        buffer[pos] = ' ';
                        Print(pos, buffer);
                    }
                } 
                else if(c.Key == ConsoleKey.Enter)
                {
                    buffer[pos] = '\n';
                    Print(pos, buffer);
                    pos++;
                }
                else
                {
                    buffer[pos] = c.KeyChar;
                    Print(pos, buffer);
                    pos++;
                    if(pos > capacity)
                    {
                        capacity *= 2;
                        Array.Resize(ref buffer, capacity);
                    }
                }
            }
        }

        private void Print(int pos, char[] buffer)
        {
            Console.SetCursorPosition(pos, 0);
            Console.Write(buffer[pos]);
        }
    }
}
