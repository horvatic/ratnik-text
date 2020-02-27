using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ratnik_text_console
{
    public interface IConsoleReader
    {
        Task Start(CancellationToken cancellationToken);
        Channel<ConsoleKeyInfo> Subscribe();
    }
}
