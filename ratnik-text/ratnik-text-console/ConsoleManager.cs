using System;
using System.Collections.Generic;

namespace ratnik_text_console
{
    public class ConsoleManager
    {
        private readonly IFileService _fileService;
        private readonly IKeyService _keyService;
        private List<List<char>> page;
        private int line;
        private int col;

        public ConsoleManager(IFileService fileService, IKeyService keyService)
        {
            Console.Clear();
            _fileService = fileService;
            _keyService = keyService;
            page = new List<List<char>>();
            line = 0;
            col = 0;
        }

        public ConsoleManager(string filePath, IFileService fileService, IKeyService keyService)
        {
            Console.Clear();
            _fileService = fileService;
            _keyService = keyService;
            page = _fileService.ReadFile(new List<List<char>>(), filePath);
            line = 0;
            col = 0;
        }

        public void Start()
        {
            var stop = false;
            while (!stop)
            {
                var c = Console.ReadKey(true);
                if (!char.IsControl(c.KeyChar))
                {
                    (col, line) = _keyService.NonControlKey(c.KeyChar, page, col, line);
                }
                else if(c.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    Console.WriteLine("Enter File Path To Save, Leave Blank To Cancel:");
                    var path = Console.ReadLine();
                    Console.WriteLine("Saving");
                    _fileService.SaveFile(page, col, line, path);
                }
                else if (c.Key == ConsoleKey.F2)
                {
                    stop = Quit(page, col, line);
                }
                else if(c.Key == ConsoleKey.F3)
                {
                    Console.Clear();
                    Console.WriteLine("Enter File Path To Open, Leave Blank To Cancel:");
                    var path = Console.ReadLine();
                    Console.WriteLine("Opening");
                    page = _fileService.ReadFile(page, path);
                    line = 0;
                    col = 0;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    (col, line) = _keyService.EnterKey(page, line);
                }
                else if (c.Key == ConsoleKey.Backspace)
                {
                    (col, line) = _keyService.Backspace(page, col, line);
                }
                else if(c.Key == ConsoleKey.LeftArrow)
                {
                    (col, line) = _keyService.LeftArrow(page, col, line);
                }
                else if (c.Key == ConsoleKey.RightArrow)
                {
                    (col, line) = _keyService.RightArrow(page, col, line);
                }
            }
        }

        private bool Quit(List<List<char>> page, int col, int line)
        {
            Console.Clear();
            Console.WriteLine("Do you want to quit: y/n");
            var shouldQuit = Console.ReadLine();
            if (!string.IsNullOrEmpty(shouldQuit) && shouldQuit.ToLower() == "y")
            {
                return true;
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            foreach (var p in page)
            {
                Console.Write(p.ToArray());
                Console.WriteLine();
            }
            Console.SetCursorPosition(col, line);
            return false;
        }
    }
}
