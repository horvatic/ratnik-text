using System.Collections.Generic;

namespace ratnik_text_console
{
    public interface IKeyService
    {
        (int col, int line) RightArrow(List<List<char>> page, int col, int line);
        (int col, int line) LeftArrow(List<List<char>> page, int col, int line);
        (int col, int line) Backspace(List<List<char>> page, int col, int line);
        (int col, int line) EnterKey(List<List<char>> page, int line);
        (int col, int line) NonControlKey(char c, List<List<char>> page, int col, int line);
    }
}
