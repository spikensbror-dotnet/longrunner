using System;

namespace LongRunner
{
    /// <summary>
    /// Represents a scheduled job.
    /// </summary>
    public interface ILongScheduledJob
    {
        /// <summary>
        /// The job that was scheduled.
        /// </summary>
        ILongJob Job { get; }

        /// <summary>
        /// The cron expression of the scheduled job.
        /// </summary>
        string Cron { get; }

        /// <summary>
        /// When the scheduled job will run next.
        /// </summary>
        DateTimeOffset? RunAtUtc { get; set; }

        /// <summary>
        /// Specifies if the job is currently running or not.
        /// </summary>
        bool Running { get; set; }
    }
}
