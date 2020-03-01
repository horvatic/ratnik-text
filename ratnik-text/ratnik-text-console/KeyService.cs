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
                    page[newLine][newCol] = ' ';
                    newLine--;
                }
                newCol = page[newLine].Count;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                foreach (var p in page)
                {
                    Console.Write(p.ToArray());
                    Console.WriteLine();
                }
                Console.SetCursorPosition(newCol, newLine);
                return (newCol, newLine);
            }
            if (newCol > 0)
            {
                newCol--;
                page[newLine][newCol] = ' ';
            }
            Console.SetCursorPosition(newCol, newLine);
            Console.Write(' ');
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
                lineText[newCol] = c;
            }
            catch
            {
                lineText.Add(c);
            }
            Console.SetCursorPosition(newCol, newLine);
            Console.Write(lineText[newCol]);
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
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            foreach (var p in page)
            {
                Console.Write(p.ToArray());
                Console.WriteLine();
            }
            Console.SetCursorPosition(newCol, newLine);
            return (newCol, newLine);
        }
    }
}
