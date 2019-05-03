using LongRunner;
using System;

namespace Autofac
{
    class JobStartup : IDisposable
    {
        private readonly ILongJobScheduler scheduler;
        private readonly IDisposable disposable;

        public JobStartup(ILongJobScheduler scheduler)
        {
            this.scheduler = scheduler;
            this.disposable = LongJobs.Run(scheduler);
        }

        public void Dispose()
        {
            using (this.disposable)
            {
                //
            }
        }
    }
}
