using System;
using System.Linq;
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

        private int currentCharNumber;

        public ConsoleManager(IConsoleOutput consoleOutput, IConsoleScreen consoleScreen, IFileBuffer fileBuffer)
        {
            _consoleOutput = consoleOutput;
            _consoleScreen = consoleScreen;
            _fileBuffer = fileBuffer;

            currentCharNumber = 0;
        }

        public async Task Start(Channel<ConsoleKeyInfo> ch, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var c = await ch.Reader.ReadAsync();
                var page = _consoleScreen.GetPage();
                Action setCurrentChar = () => { };
                var (x, y) = _consoleScreen.GetCursorPosition();
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (currentCharNumber > 0)
                    {
                        currentCharNumber--;
                    }
                    _fileBuffer.Insert(currentCharNumber, page, ' ');
                } 
                else if(c.Key == ConsoleKey.Enter)
                {
                    _fileBuffer.Insert(currentCharNumber, page, '\n');
                    setCurrentChar = () => currentCharNumber++;
                }
                else if(!char.IsControl(c.KeyChar))
                {
                    _fileBuffer.Insert(currentCharNumber, page, c.KeyChar);
                    setCurrentChar = () => currentCharNumber++;
                }
                //else if(c.Key == ConsoleKey.LeftArrow || c.Key == ConsoleKey.RightArrow)
                //{
                //    if(c.Key == ConsoleKey.LeftArrow)
                //    {
                //        _consoleScreen.SetLeftArrow();
                //    }
                //    else
                //    {
                //        _consoleScreen.SetRightArrow();
                //    }
                //    var arrowPage = _consoleScreen.GetPage();

                //    var filePage = _fileBuffer.ReadPage(arrowPage);
                //    var testpoint = c.Key == ConsoleKey.LeftArrow ? currentCharNumber - 1 : currentCharNumber + 1;

                //    if (filePage[testpoint] != '\0')
                //    {
                //        (x, y) = _consoleScreen.GetCursorPosition();
                //        Console.SetCursorPosition(x, y);
                //        _consoleOutput.SetPage(arrowPage);
                //        if(c.Key == ConsoleKey.LeftArrow)
                //        {
                //            setCurrentChar = () => currentCharNumber--;
                //        }
                //        else
                //        {
                //            setCurrentChar = () => currentCharNumber++;
                //        }
                //    }
                //    else
                //    {
                //        if (c.Key == ConsoleKey.LeftArrow)
                //        {
                //            _consoleScreen.UnsetLeftArrow();
                //        }
                //        else
                //        {
                //            _consoleScreen.UnsetRightArrow();
                //        }
                //        (x, y) = _consoleScreen.GetCursorPosition();
                //        Console.SetCursorPosition(x, y);
                //    }
                //    setCurrentChar();
                //    continue;
                //}

                if (c.Key == ConsoleKey.LeftArrow)
                {

                    setCurrentChar = () => currentCharNumber--;
                    if (currentCharNumber == 0)
                    {
                        _consoleScreen.SetBackspace(0);
                    }
                    else
                    {
                        var backIndex = currentCharNumber - 1;
                        var backPage = _consoleScreen.GetPage();
                        var backFile = _fileBuffer.ReadPage(backPage);
                        while (backFile[backIndex] == '\n')
                        {
                            if (backIndex == 0 && backPage == 0)
                            {
                                break;
                            }
                            else if (backIndex == 0)
                            {
                                backPage--;
                                backFile = _fileBuffer.ReadPage(backPage);
                                backIndex = backFile.Count(foundChar => foundChar != '\0') - 1;
                            }
                            else
                            {
                                backIndex--;
                            }
                        }
                        _consoleScreen.SetBackspace(currentCharNumber + 2 - backIndex);
                    }
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    _consoleScreen.SetEnter();
                }
                else if (!char.IsControl(c.KeyChar))
                {
                    _consoleScreen.SetChar();
                }
                _consoleOutput.PrintPage(x, y, _consoleScreen.GetPage(), _fileBuffer);
                (x, y) = _consoleScreen.GetCursorPosition();
                Console.SetCursorPosition(x, y);
                
                if (_consoleScreen.GetPage() != page)
                {
                    setCurrentChar = () => currentCharNumber = 0;
                }

                setCurrentChar();
            }
        }
    }
}
