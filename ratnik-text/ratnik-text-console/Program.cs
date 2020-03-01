using System.Threading;
using System.Threading.Tasks;

namespace ratnik_text_console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var token = new CancellationTokenSource();
            var reader = new ConsoleReader();
            var screen = new ConsoleScreen();
            var output = new ConsoleOutput();
            var fileBuffer = new FileBuffer();
            var ch1 = reader.Subscribe();
            var consoleManager = new ConsoleManager(output, screen, fileBuffer);

            var consoleManagerTask = Task.Factory.StartNew(async () => await consoleManager.Start(ch1, token.Token));
            var readerTask = Task.Factory.StartNew(async () => await reader.Start(token.Token));

            await Task.WhenAll(readerTask, consoleManagerTask);
        }
    }
}
