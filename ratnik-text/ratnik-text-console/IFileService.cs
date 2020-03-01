using System.Collections.Generic;

namespace ratnik_text_console
{
    public interface IFileService
    {
        void SaveFile(List<List<char>> page, int col, int line, string path);
        List<List<char>> ReadFile(string path);
    }
}
