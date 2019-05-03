using System;

namespace LongRunner
{
    public interface ILongScheduledJob
    {
        ILongJob Job { get; }
        string Cron { get; }
        DateTimeOffset? RunAtUtc { get; set; }
        bool Running { get; set; }
    }
}
