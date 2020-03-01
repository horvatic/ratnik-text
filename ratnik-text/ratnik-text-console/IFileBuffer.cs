namespace ratnik_text_console
{
    public interface IFileBuffer
    {
        void Insert(int pos, int page, char c);
        char[] ReadPage(int page);
     }
}
