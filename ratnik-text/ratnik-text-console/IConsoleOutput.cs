namespace ratnik_text_console
{
    public interface IConsoleOutput
    {
        void Print((int xPos, int yPos) cusPos, int bufferPos, char[] buffer);
    }
}
