using System;
using System.Threading.Tasks;

namespace LongRunner.ExampleApp
{
    class ConsoleWriteTestJob : ILongJob
    {
        public async Task Execute()
        {
            await Console.Out.WriteLineAsync($"Console write test");
        }
    }
}
