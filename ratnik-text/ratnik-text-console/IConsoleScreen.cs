using System;

namespace ratnik_text_console
{
    public interface IConsoleScreen
    {
        void SetCursorPosition(ConsoleKeyInfo c);
        void SetCursorPositionOnNewPage(int newPage);
        (int x, int y) GetCursorPosition();
        int GetPage();
    }
}
