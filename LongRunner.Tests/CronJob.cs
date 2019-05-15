using System.Threading.Tasks;

namespace LongRunner.Tests
{
    class CronJob : ILongJob
    {
        public static int Invokations { get; set; }

        public async Task Execute()
        {
            Invokations++;
        }
    }
}
