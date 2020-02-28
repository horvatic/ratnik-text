namespace ratnik_text_console
{
    public interface IConsoleOutput
    {
        void Print(IFileBuffer fileBuffer);
        void PrintOnNewPage(int page, IFileBuffer fileBuffer);
    }
}
