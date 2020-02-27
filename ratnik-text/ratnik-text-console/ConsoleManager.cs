using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ratnik_text_console
{
    public class ConsoleManager
    {
        private readonly IConsoleOutput _consoleOutput;
        private readonly ICursor _cursor;

        public ConsoleManager(IConsoleOutput consoleOutput, ICursor cursor)
        {
            _consoleOutput = consoleOutput;
            _cursor = cursor;
        }

        public async Task Start(Channel<ConsoleKeyInfo> ch, CancellationToken cancellationToken)
        {
            var capacity = 1028;
            var buffer = new char[capacity];
            var pos = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var c = await ch.Reader.ReadAsync();
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (pos != 0)
                    {
                        pos -= 1;
                        buffer[pos] = ' ';
                        _consoleOutput.Print(_cursor.GetCursorPosition(), pos, buffer);
                    }
                    else
                    {
                        buffer[pos] = ' ';
                        _consoleOutput.Print(_cursor.GetCursorPosition(), pos, buffer);
                    }
                } 
                else if(c.Key == ConsoleKey.Enter)
                {
                    buffer[pos] = '\n';
                    pos++;
                    if (pos > capacity)
                    {
                        capacity *= 2;
                        Array.Resize(ref buffer, capacity);
                    }
                }
                else if(!char.IsControl(c.KeyChar))
                {
                    buffer[pos] = c.KeyChar;
                    _consoleOutput.Print(_cursor.GetCursorPosition(), pos, buffer);
                    pos++;
                    if(pos > capacity)
                    {
                        capacity *= 2;
                        Array.Resize(ref buffer, capacity);
                    }
                }
                _cursor.SetPos(c);
            }
        }
    }
}
