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
                var pos = _consoleScreen.GetCursorPosition();
                _consoleScreen.SetCursorPosition(c);
                var triggerNewPage = false;
                if (_consoleScreen.GetPage() != page)
                {
                    triggerNewPage = true;
                    page = _consoleScreen.GetPage();
                    pos = _consoleScreen.GetCursorPosition();
                }
                if (c.Key == ConsoleKey.Backspace)
                {
                    pos = _consoleScreen.GetCursorPosition();
                    _fileBuffer.Insert(pos, page, ' ');
                } 
                else if(c.Key == ConsoleKey.Enter)
                {
                    _fileBuffer.Insert(pos, page, '\n');
                }
                else if(!char.IsControl(c.KeyChar))
                {
                    _fileBuffer.Insert(pos, page, c.KeyChar);
                }
                else
                {
                    pos = _consoleScreen.GetCursorPosition();            
                    Console.SetCursorPosition(pos.x, pos.y);
                }

                if (triggerNewPage)
                {
                    _consoleOutput.PrintOnNewPage(_consoleScreen.GetPage(), _fileBuffer);
                }
                else
                {
                    _consoleOutput.Print(pos, _fileBuffer);
                }
            }
        }
    }
}
