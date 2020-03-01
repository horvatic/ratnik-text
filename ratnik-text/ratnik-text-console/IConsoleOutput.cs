namespace ratnik_text_console
{
    public interface IConsoleOutput
    {
        void PrintPage(int xCusorPos, int yCusorPos, int page, IFileBuffer fileBuffer);
        void SetPage(int page);
    }
}
