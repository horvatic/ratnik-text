using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ratnik_text_console
{
    public class ConsoleReader : IConsoleReader
    {
        private readonly List<Channel<ConsoleKeyInfo>> _subscriber;

        public ConsoleReader()
        {
            _subscriber = new List<Channel<ConsoleKeyInfo>>();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var c = Console.ReadKey(true);
                foreach (var ch in _subscriber)
                {
                    await ch.Writer.WriteAsync(c);
                }
            }
        }

        public Channel<ConsoleKeyInfo> Subscribe()
        {
            var ch = Channel.CreateUnbounded<ConsoleKeyInfo>();

            _subscriber.Add(ch);

            return ch;
        }
    }
}
