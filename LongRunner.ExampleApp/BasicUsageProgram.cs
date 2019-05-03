using LongRunner;
using System;
using System.Threading.Tasks;

namespace LongRunner.ExampleApp
{
    static class BasicUsageProgram
    {
        public static void Run(string[] args)
        {
            // Every minute.
            var scheduler = LongJobs.CreateScheduler();
            scheduler.Schedule<ConsoleWriteTestJob>("* * * * *");

            // Every five minutes.
            var otherScheduler = LongJobs.CreateScheduler();
            otherScheduler.Schedule<OtherConsoleWriteTestJob>("0 0/5 * 1/1 * ? *");

            using (LongJobs.Run(scheduler))
            using (LongJobs.Run(otherScheduler))
            {
                Console.ReadKey(true);
            }
        }
    }
}
