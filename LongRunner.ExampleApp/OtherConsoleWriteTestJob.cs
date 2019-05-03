using System;
using System.Threading.Tasks;

namespace LongRunner.ExampleApp
{
    class OtherConsoleWriteTestJob : ILongJob
    {
        public async Task Execute()
        {
            await Console.Out.WriteLineAsync($"Other console write test");
        }
    }
}
