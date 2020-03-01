using System;
using System.Collections.Generic;
using System.Linq;

namespace ratnik_text_console
{
    public class KeyService : IKeyService
    {
        public (int col, int line) RightArrow(List<List<char>> page, int col, int line)
        {
            var newCol = col;
            var newLine = line;
            if (newCol == page[newLine].Count && newLine == page.Count - 1)
            {
                return (newCol, newLine);
            }
            if (newCol == page[newLine].Count)
            {
                newLine++;
                newCol = 0;
            }
            else
            {
                newCol++;
            }
            Console.SetCursorPosition(newCol, newLine);
            return (newCol, newLine);
        }

        public (int col, int line) LeftArrow(List<List<char>> page, int col, int line)
        {
            var newCol = col;
            var newLine = line;
            if (newCol <= 0 && newLine <= 0)
            {
                return (newCol, newLine);
            }
            if (newCol <= 0)
            {
                newLine--;
                newCol = page[newLine].Count;
            }
            if (newCol > 0)
            {
                newCol--;
            }
            Console.SetCursorPosition(newCol, newLine);
            return (newCol, newLine);
        }

        public (int col, int line) Backspace(List<List<char>> page, int col, int line)
        {
            var newCol = col;
            var newLine = line;
            if (newCol <= 0 && newLine <= 0)
            {
                return (newCol, newLine);
            }
            if (newCol <= 0)
            {
                if (page[newLine].Count == 0 || page[newLine].All(x => x == ' '))
                {
                    page.RemoveAt(newLine);
                    newLine--;
                }
                else
                {
                    page[newLine].RemoveAt(newCol);
                    newLine--;
                }
                newCol = page[newLine].Count;
                var clearLine = newLine;
                var range = page.Count - newLine;
                foreach (var p in page.GetRange(newLine, range))
                {
                    Console.SetCursorPosition(0, clearLine);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, clearLine);
                    Console.Write(p.ToArray());
                    Console.WriteLine();
                    clearLine++;
                }
                Console.SetCursorPosition(0, page.Count);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(newCol, newLine);
                return (newCol, newLine);
            }
            if (newCol > 0)
            {
                newCol--;
                page[newLine].RemoveAt(newCol);
            }
            Console.SetCursorPosition(0, newLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, newLine);
            Console.Write(page[newLine].ToArray());
            Console.SetCursorPosition(newCol, newLine);
            return (newCol, newLine);
        }

        public (int col, int line) EnterKey(List<List<char>> page, int line)
        {
            return InsertNewLine(page, line);
        }

        public (int col, int line) NonControlKey(char c, List<List<char>> page, int col, int line)
        {
            var newCol = col;
            var newLine = line;
            var lineText = page.ElementAtOrDefault(newLine);
            while (lineText == null)
            {
                page.Add(new List<char>());
                lineText = page.ElementAtOrDefault(newLine);
            }
            try
            {
                lineText.Insert(newCol, c);
            }
            catch
            {
                lineText.Add(c);
            }
            Console.SetCursorPosition(0, newLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, newLine);
            Console.Write(lineText.ToArray());
            Console.SetCursorPosition(newCol + 1, newLine);
            newCol++;
            if (newCol >= Console.WindowWidth - 1)
            {
                return InsertNewLine(page, newLine);
            }
            return (newCol, newLine);
        }

        private (int col, int line) InsertNewLine(List<List<char>> page, int line)
        {
            var newCol = 0;
            var newLine = line + 1;
            try
            {
                page.Insert(newLine, new List<char>());
            }
            catch
            {
                page.Add(new List<char>());
            }
            var clearLine = newLine - 1;
            var range = page.Count - clearLine;
            foreach (var p in page.GetRange(clearLine, range))
            {
                Console.SetCursorPosition(0, clearLine);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, clearLine);
                Console.Write(p.ToArray());
                Console.WriteLine();
                clearLine++;
            }
            Console.SetCursorPosition(newCol, newLine);
            return (newCol, newLine);
        }
    }
}
