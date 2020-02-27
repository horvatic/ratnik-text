using System;

namespace ratnik_text_console
{
    public interface ICursor
    {
        void SetPos(ConsoleKeyInfo c);
        (int x, int y) GetCursorPosition();
    }
}
