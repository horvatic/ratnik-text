using System;

namespace ratnik_text_console
{
    public interface IConsoleScreen
    {
        void SetEnter();
        void SetChar();
        void SetBackspace(int x);
        void SetLeftArrow();
        void SetRightArrow();
        void UnsetLeftArrow();
        void UnsetRightArrow();
        (int x, int y) GetCursorPosition();
        int GetPage();
    }
}
