namespace LongRunner
{
    public interface ILongJobScheduler
    {
        void Schedule<TJob>(string cron) where TJob : ILongJob;
        void Unschedule(ILongScheduledJob scheduledJob);
        ILongScheduledJob[] Get();
    }
}
