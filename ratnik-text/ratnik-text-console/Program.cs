using System.Threading;
using System.Threading.Tasks;

namespace ratnik_text_console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var token = new CancellationTokenSource();
            var output = new ConsoleOutput();
            var reader = new ConsoleReader();
            var cursor = new Cursor();
            var ch1 = reader.Subscribe();
            var consoleManager = new ConsoleManager(output, cursor);

            var consoleManagerTask = Task.Factory.StartNew(async () => await consoleManager.Start(ch1, token.Token));
            var readerTask = Task.Factory.StartNew(async () => await reader.Start(token.Token));

            await Task.WhenAll(readerTask, consoleManagerTask);
        }
    }
}
