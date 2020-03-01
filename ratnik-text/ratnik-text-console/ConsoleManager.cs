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
                var c = System.Console.ReadKey(true);
                if (!char.IsControl(c.KeyChar))
                {
                    NonControlKey(c.KeyChar);
                }
                else if (c.Key == System.ConsoleKey.Enter)
                {
                    EnterKey();
                }
                else if (c.Key == System.ConsoleKey.Backspace)
                {
                    Backspace();
                }
                else if(c.Key == System.ConsoleKey.LeftArrow)
                {
                    LeftArrow();
                }
                else if (c.Key == System.ConsoleKey.RightArrow)
                {
                    RightArrow();
                }
            }
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
                col = page[line].Count;
            }
            else
            {
                col++;
            }
            System.Console.SetCursorPosition(col, line);
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
            System.Console.SetCursorPosition(col, line);
        }

        private void Backspace()
        {
            if (col <= 0 && line <= 0)
            {
                return;
            }
            if (col <= 0)
            {
                if(line == page.Count - 1)
                {
                    page.RemoveAt(line);
                }
                line--;
                col = page[line].Count;
            }
            if (col > 0)
            {
                col--;
                page[line].RemoveAt(col);
            }
            System.Console.SetCursorPosition(col, line);
            System.Console.Write(' ');
            System.Console.SetCursorPosition(col, line);
        }

        private void EnterKey()
        {
            col = 0;
            line++;
            var lineText = page.ElementAtOrDefault(line);
            while (lineText == null)
            {
                page.Add(new List<char>());
                lineText = page.ElementAtOrDefault(line);
            }
            System.Console.SetCursorPosition(col, line);
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
            System.Console.SetCursorPosition(col, line);
            System.Console.Write(lineText[col]);
            col++;
        }
    }
}
