using System;

namespace LongRunner
{
    public static class LongJobs
    {
        public static ILongJobActivator DefaultJobActivator { get; set; }

        static LongJobs()
        {
            DefaultJobActivator = new DefaultJobActivator();
        }

        public static ILongJobScheduler CreateScheduler(ILongJobActivator jobActivator = null)
        {
            return new InMemoryJobScheduler(jobActivator ?? DefaultJobActivator);
        }

        public static IDisposable Run(ILongJobScheduler scheduler)
        {
            return new LongJobThread(scheduler);
        }
    }
}
