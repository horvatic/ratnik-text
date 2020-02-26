using System.Threading.Tasks;

namespace ratnik_text_console
{
    class Program
    {
        static async Task Main()
        {
            var readerTask = new TaskFactory().StartNew(() => new InputReader().Start());
            await readerTask;
        }
    }
}
