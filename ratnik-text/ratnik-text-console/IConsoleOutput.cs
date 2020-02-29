namespace ratnik_text_console
{
    public interface IConsoleOutput
    {
        void Print((int x, int y) pos, IFileBuffer fileBuffer);
        void PrintOnNewPage(int page, IFileBuffer fileBuffer);
    }
}
