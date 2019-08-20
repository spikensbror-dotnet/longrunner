using System;

namespace LongRunner
{
    /// <summary>
    /// Manages the schedule for a specific LongRunner instance.
    /// </summary>
    public interface ILongJobScheduler
    {
        /// <summary>
        /// Schedules the specified job to run according to the given cron expression. If the job
        /// is still running by the scheduled start, it will be skipped until it has stopped
        /// execution.
        /// </summary>
        /// <typeparam name="TJob">The type of job to schedule.</typeparam>
        /// <param name="cron">The CRON expression of the scheduled job.</param>
        /// <param name="runAtUtc">When the scheduled job will start running.</param>
        void Schedule<TJob>(string cron, DateTimeOffset? runAtUtc = null) where TJob : ILongJob;

        /// <summary>
        /// Schedules the specified job to run once (fire and forget) at the given time.
        /// </summary>
        /// <typeparam name="TJob">The type of job to schedule.</typeparam>
        /// <param name="runAtUtc">When the job should run.</param>
        void Schedule<TJob>(DateTimeOffset runAtUtc) where TJob : ILongJob;

        /// <summary>
        /// Unschedules a scheduled job.
        /// </summary>
        /// <param name="scheduledJob">The scheduled job to unschedule</param>
        void Unschedule(ILongScheduledJob scheduledJob);

        /// <summary>
        /// Gets all scheduled jobs.
        /// </summary>
        /// <returns>All scheduled jobs.</returns>
        ILongScheduledJob[] Get();
    }
}
