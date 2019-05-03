using Cronos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunner
{
    class LongJobThread : IDisposable
    {
        private readonly Thread thread;
        private readonly ILongJobScheduler scheduler;

        private bool ShouldStop { get; set; }

        public LongJobThread(ILongJobScheduler scheduler)
        {
            this.thread = new Thread(new ThreadStart(this.Run));
            this.scheduler = scheduler;

            this.thread.Start();
        }

        public void Dispose()
        {
            this.ShouldStop = true;
            this.thread.Join();
        }

        private void Run()
        {
            while (!this.ShouldStop)
            {
                foreach (var scheduledJob in this.scheduler.Get())
                {
                    // If the job has not been scheduled to run and has no cron expression to derive next
                    // run date from.
                    if (!scheduledJob.RunAtUtc.HasValue && string.IsNullOrEmpty(scheduledJob.Cron))
                    {
                        this.scheduler.Unschedule(scheduledJob);
                        continue;
                    }
                    // If the job has not been scheduled to run yet but can be scheduled using its cron
                    // expression.
                    else if (!scheduledJob.RunAtUtc.HasValue && !string.IsNullOrEmpty(scheduledJob.Cron))
                    {
                        var expression = CronExpression.Parse(scheduledJob.Cron);
                        scheduledJob.RunAtUtc = expression.GetNextOccurrence(DateTime.UtcNow);
                    }
                    // If the job is not running and we have passed the scheduled run date. This means that
                    // if a job is still running by the next run date, it will keep running the current
                    // execution without starting a new one. This may incur indefinately until execution
                    // completes.
                    else if (!scheduledJob.Running && scheduledJob.RunAtUtc.Value <= DateTimeOffset.UtcNow)
                    {
                        Task.Factory.StartNew(async () =>
                        {
                            scheduledJob.Running = true;

                            await scheduledJob.Job.Execute();

                            scheduledJob.Running = false;
                        }, TaskCreationOptions.LongRunning);

                        // Re-schedule next run date if cron expression is present.
                        if (!string.IsNullOrEmpty(scheduledJob.Cron))
                        {
                            var expression = CronExpression.Parse(scheduledJob.Cron);
                            scheduledJob.RunAtUtc = expression.GetNextOccurrence(DateTime.UtcNow);
                        }
                        else
                        {
                            // Let it get unscheduled next roundtrip. (Fire and forget)
                            scheduledJob.RunAtUtc = null;
                        }
                    }
                }

                Thread.Sleep(50);
            }
        }
    }
}
