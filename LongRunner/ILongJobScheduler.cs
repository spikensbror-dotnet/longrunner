using System;

namespace LongRunner
{
    public interface ILongJobScheduler
    {
        void Schedule<TJob>(string cron, DateTimeOffset? runAtUtc = null) where TJob : ILongJob;
        void Schedule<TJob>(DateTimeOffset runAtUtc) where TJob : ILongJob;
        void Unschedule(ILongScheduledJob scheduledJob);
        ILongScheduledJob[] Get();
    }
}
