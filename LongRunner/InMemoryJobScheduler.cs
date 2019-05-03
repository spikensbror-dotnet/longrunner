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

        public void Schedule<TJob>(string cron) where TJob : ILongJob
        {
            this.scheduledJobs.Add(new ScheduledJob(this.jobActivator.Activate(typeof(TJob)), cron));
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
