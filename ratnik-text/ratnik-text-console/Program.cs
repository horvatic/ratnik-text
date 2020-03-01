namespace ratnik_text_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileService = new FileService();
            var keyService = new KeyService();
            if (args.Length > 0)
            {
                var console = new ConsoleManager(args[0], fileService, keyService);
                console.Start();
            }
            else
            {
                var console = new ConsoleManager(fileService, keyService);
                console.Start();
            }
        }
    }
}
