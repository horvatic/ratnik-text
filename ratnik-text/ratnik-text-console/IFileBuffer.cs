namespace ratnik_text_console
{
    public interface IFileBuffer
    {
        void Insert((int xPos, int yPos) pos, int page, char c);
        char[] ReadPage(int page);
        char Read((int xPos, int yPos) pos, int page);
    }
}
