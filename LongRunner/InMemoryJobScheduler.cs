using System;
using System.Collections.Generic;

namespace LongRunner
{
    class InMemoryJobScheduler : ILongJobScheduler
    {
        private readonly List<ILongScheduledJob> scheduledJobs;
        private readonly ILongJobActivator jobActivator;

        public InMemoryJobScheduler(ILongJobActivator jobActivator)
        {
            this.scheduledJobs = new List<ILongScheduledJob>();
            this.jobActivator = jobActivator;
        }

        public ILongScheduledJob[] Get()
        {
            return this.scheduledJobs.ToArray();
        }

        public void Schedule<TJob>(string cron, DateTimeOffset? runAtUtc = null) where TJob : ILongJob
        {
            this.scheduledJobs.Add(new ScheduledJob(this.jobActivator.Activate(typeof(TJob)), cron)
            {
                RunAtUtc = runAtUtc,
            });
        }

        public void Schedule<TJob>(DateTimeOffset runAtUtc) where TJob : ILongJob
        {
            this.scheduledJobs.Add(new ScheduledJob(this.jobActivator.Activate(typeof(TJob)), null)
            {
                RunAtUtc = runAtUtc,
            });
        }

        public void Unschedule(ILongScheduledJob scheduledJob)
        {
            this.scheduledJobs.Remove(scheduledJob);
        }

        class ScheduledJob : ILongScheduledJob
        {
            public ILongJob Job { get; }
            public string Cron { get; }
            public DateTimeOffset? RunAtUtc { get; set; }
            public bool Running { get; set; }

            public ScheduledJob(ILongJob job, string cron)
            {
                this.Job = job;
                this.Cron = cron;
                this.RunAtUtc = null;
                this.Running = false;
            }
        }
    }
}
