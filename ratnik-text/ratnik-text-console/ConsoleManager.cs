using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ratnik_text_console
{
    public class ConsoleManager
    {
        private readonly IConsoleOutput _consoleOutput;
        private readonly IConsoleScreen _consoleScreen;
        private readonly IFileBuffer _fileBuffer;

        public ConsoleManager(IConsoleOutput consoleOutput, IConsoleScreen consoleScreen, IFileBuffer fileBuffer)
        {
            _consoleOutput = consoleOutput;
            _consoleScreen = consoleScreen;
            _fileBuffer = fileBuffer;
        }

        public async Task Start(Channel<ConsoleKeyInfo> ch, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var c = await ch.Reader.ReadAsync();
                var page = _consoleScreen.GetPage();
                if (c.Key == ConsoleKey.Backspace)
                {
                    _fileBuffer.Insert(_consoleScreen.GetCursorPosition(), _consoleScreen.GetPage(), ' ');
                } 
                else if(c.Key == ConsoleKey.Enter)
                {
                    _fileBuffer.Insert(_consoleScreen.GetCursorPosition(), _consoleScreen.GetPage(), '\n');
                }
                else if(!char.IsControl(c.KeyChar))
                {
                    _fileBuffer.Insert(_consoleScreen.GetCursorPosition(), _consoleScreen.GetPage(), c.KeyChar);
                }

                _consoleOutput.Print(_fileBuffer);

                _consoleScreen.SetCursorPosition(c);
                if (_consoleScreen.GetPage() != page)
                {
                    _consoleOutput.PrintOnNewPage(_consoleScreen.GetPage(), _fileBuffer);
                }
            }
        }
    }
}
