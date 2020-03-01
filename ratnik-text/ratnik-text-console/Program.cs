using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ratnik_text_console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var console = new ConsoleManager();
            console.Start();
        }
    }
}
