using System;
using System.Collections.Generic;
using System.Linq;

namespace ratnik_text_console
{
    public class ConsoleManager
    {
        private readonly List<List<char>> page;
        private int line;
        private int col;

        public ConsoleManager()
        {
            page = new List<List<char>>(); ;
            line = 0;
            col = 0;
        }

        public void Start()
        {
            while (true)
            {
                var c = Console.ReadKey(true);
                if (!char.IsControl(c.KeyChar))
                {
                    NonControlKey(c.KeyChar);
                }
                else if(c.Key == ConsoleKey.F1)
                {
                    SaveFile();
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    EnterKey();
                }
                else if (c.Key == ConsoleKey.Backspace)
                {
                    Backspace();
                }
                else if(c.Key == ConsoleKey.LeftArrow)
                {
                    LeftArrow();
                }
                else if (c.Key == ConsoleKey.RightArrow)
                {
                    RightArrow();
                }
            }
        }

        private void SaveFile()
        {
            Console.Clear();
            Console.WriteLine("Enter File Path:");
            var path = Console.ReadLine();
            Console.WriteLine("Saving");
            using var file = new System.IO.StreamWriter(path);
            foreach (var line in page)
            {
                file.WriteLine(line.ToArray());
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            foreach (var p in page)
            {
                Console.Write(p.ToArray());
                Console.WriteLine();
            }
            Console.SetCursorPosition(col, line);
        }

        private void RightArrow()
        {
            if (col == page[line].Count && line == page.Count - 1)
            {
                return;
            }
            if (col == page[line].Count)
            {
                line++;
                col = 0;
            }
            else
            {
                col++;
            }
            Console.SetCursorPosition(col, line);
        }

        private void LeftArrow()
        {
            if (col <= 0 && line <= 0)
            {
                return;
            }
            if (col <= 0)
            {
                line--;
                col = page[line].Count;
            }
            if (col > 0)
            {
                col--;
            }
            Console.SetCursorPosition(col, line);
        }

        private void Backspace()
        {
            if (col <= 0 && line <= 0)
            {
                return;
            }
            if (col <= 0)
            {
                page.RemoveAt(line);
                line--;
                col = page[line].Count;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                foreach (var p in page)
                {
                    Console.Write(p.ToArray());
                    Console.WriteLine();
                }
                Console.SetCursorPosition(col, line);
                return;
            }
            if (col > 0)
            {
                col--;
                page[line].RemoveAt(col);
            }
            Console.SetCursorPosition(col, line);
            Console.Write(' ');
            Console.SetCursorPosition(col, line);
        }

        private void EnterKey()
        {
            InsertNewLine();
        }

        private void NonControlKey(char c)
        {
            var lineText = page.ElementAtOrDefault(line);
            while (lineText == null)
            {
                page.Add(new List<char>());
                lineText = page.ElementAtOrDefault(line);
            }
            try
            {
                lineText[col] = c;
            }
            catch
            {
                lineText.Add(c);
            }
            Console.SetCursorPosition(col, line);
            Console.Write(lineText[col]);
            col++;
            if(col >= Console.WindowWidth - 1)
            {
                InsertNewLine();
            }
        }

        private void InsertNewLine()
        {
            col = 0;
            line++;
            try
            {
                page.Insert(line, new List<char>());
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
            Console.SetCursorPosition(col, line);
        }
    }
}
