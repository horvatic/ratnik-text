﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ratnik_text_console
{
    public class FileService : IFileService
    {
        public List<List<char>> ReadFile(string path)
        {
            var page = new List<List<char>>();
            try
            {
                using var file = new System.IO.StreamReader(path);
                string fileLine;
                while ((fileLine = file.ReadLine()) != null)
                {
                    var newLine = new List<char>();
                    newLine.AddRange(fileLine.ToArray());
                    page.Add(newLine);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Could Not Read File. Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                return page;
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            foreach (var p in page)
            {
                Console.Write(p.ToArray());
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, 0);
            return page;
        }

        public void SaveFile(List<List<char>> page, int col, int line, string path)
        {
            using var file = new System.IO.StreamWriter(path);
            foreach (var fileLine in page)
            {
                file.WriteLine(fileLine.ToArray());
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