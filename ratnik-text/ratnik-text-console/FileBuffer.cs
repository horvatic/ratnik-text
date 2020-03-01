using System;
using System.Collections.Generic;
using System.Linq;

namespace ratnik_text_console
{
    public class FileBuffer : IFileBuffer
    {
        private readonly List<char[]> pages;

        public FileBuffer()
        {
            pages = new List<char[]>();
        }

        public void Insert(int pos, int page, char c)
        {
            var buffer = pages.ElementAtOrDefault(page);
            if(buffer == null)
            {
                buffer = new char[Console.WindowHeight * Console.WindowWidth];
                pages.Add(buffer);
            }

            while(pos >= buffer.Length)
            {
                var capacity = buffer.Length * 2;
                Array.Resize(ref buffer, capacity);
            }
            buffer[pos] = c;
        }

        public char[] ReadPage(int page)
        {
            var buffer = pages.ElementAtOrDefault(page);
            if (buffer == null)
                return new char[Console.WindowHeight * Console.WindowWidth];
            else
                return buffer;
        }
    }
}
