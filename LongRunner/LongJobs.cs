using System;

namespace LongRunner
{
    /// <summary>
    /// LongRunner is a job scheduler made specifically for long-running background jobs scheduled
    /// using cron expressions.
    /// </summary>
    public static class LongJobs
    {
        /// <summary>
        /// The default job activator used by schedulers created by LongRunner.
        /// </summary>
        public static ILongJobActivator DefaultJobActivator { get; set; }

        static LongJobs()
        {
            DefaultJobActivator = new DefaultJobActivator();
        }

        /// <summary>
        /// Creates a new LongRunner job scheduler.
        /// </summary>
        /// <param name="jobActivator">A specific job activator to use for the scheduler instance or null to use the default.</param>
        /// <returns></returns>
        public static ILongJobScheduler CreateScheduler(ILongJobActivator jobActivator = null)
        {
            return new InMemoryJobScheduler(jobActivator ?? DefaultJobActivator);
        }

        /// <summary>
        /// Runs the scheduler on a new thread that is stopped when the returned handle is disposed.
        /// </summary>
        /// <param name="scheduler">The job scheduler to run.</param>
        /// <returns>A handle that stops the scheduler when disposed.</returns>
        public static IDisposable Run(ILongJobScheduler scheduler)
        {
            return new LongJobThread(scheduler);
        }
    }
}
