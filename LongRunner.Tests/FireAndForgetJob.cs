using System.Threading.Tasks;

namespace LongRunner.Tests
{
    class FireAndForgetJob : ILongJob
    {
        public static int Invokations { get; set; }

        public async Task Execute()
        {
            Invokations++;
        }
    }
}
