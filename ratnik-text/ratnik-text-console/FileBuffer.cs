﻿using System;

namespace ratnik_text_console
{
    public class FileBuffer : IFileBuffer
    {
        private char[] buffer;
        private int capacity;

        public FileBuffer()
        {
            capacity = 1028;
            buffer = new char[capacity];
        }

        public void Insert((int xPos, int yPos) pos, int page, char c)
        {
            var bufferPos = (pos.yPos * Console.WindowWidth) + pos.xPos + (page * Console.WindowHeight * Console.WindowWidth);
            while(bufferPos >= capacity)
            {
                capacity *= 2;
                Array.Resize(ref buffer, capacity);
            }
            buffer[bufferPos] = c;
        }

        public char Read((int xPos, int yPos) pos, int page)
        {
            var bufferPos = (pos.yPos * Console.WindowWidth) + pos.xPos + (page * Console.WindowHeight * Console.WindowWidth);
            if (bufferPos < buffer.Length)
            {
                return buffer[bufferPos];
            }
            return ' ';
        }

        public char[] ReadPage(int page)
        {
            var bufferRange = new char[Console.WindowHeight * Console.WindowWidth];
            var pagePos = page * Console.WindowHeight * Console.WindowWidth;
            for (var y = 0; y < Console.WindowHeight; y++)
            {
                for (var x = 0; x < Console.WindowWidth; x++)
                {
                    var bufferPos = x + (y * Console.WindowWidth) + pagePos;
                    if (bufferPos < buffer.Length)
                    {
                        bufferRange[x + (y * Console.WindowWidth)] = buffer[bufferPos];
                    } 
                    else
                    {
                        bufferRange[x + (y * Console.WindowWidth)] = ' ';
                    }
                }
            }
            return bufferRange;
        }
    }
}