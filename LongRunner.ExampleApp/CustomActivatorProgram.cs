using LongRunner;
using System;

namespace LongRunner.ExampleApp
{
    static class CustomActivatorProgram
    {
        public static void Run(string[] args)
        {
            LongJobs.DefaultJobActivator = new DefaultJobActivator();

            var scheduler = LongJobs.CreateScheduler();
            scheduler.Schedule<ConsoleWriteTestJob>("* * * * *");

            var otherScheduler = LongJobs.CreateScheduler(new OtherJobActivator());
            otherScheduler.Schedule<ConsoleWriteTestJob>("*/2 * * * *");

            using (LongJobs.Run(scheduler))
            using (LongJobs.Run(otherScheduler))
            {
                Console.ReadKey(true);
            }
        }

        class DefaultJobActivator : ILongJobActivator
        {
            ILongJob ILongJobActivator.Activate(Type jobType)
            {
                Console.WriteLine($"Activated long running job {jobType.Name} from my default job activator!");

                return (ILongJob)Activator.CreateInstance(jobType);
            }
        }

        class OtherJobActivator : ILongJobActivator
        {
            ILongJob ILongJobActivator.Activate(Type jobType)
            {
                Console.WriteLine($"Activated long running job {jobType.Name} from my other job activator!");

                return (ILongJob)Activator.CreateInstance(jobType);
            }
        }
    }
}
