using System;
using System.Threading;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LongRunner.Tests
{
    [TestClass]
    public class LongJobsTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            CronJob.Invokations = 0;
            FireAndForgetJob.Invokations = 0;
        }

        [TestMethod]
        public void ShouldBeAbleToScheduleAndRunJobs()
        {
            var scheduler = LongJobs.CreateScheduler();

            // Scheduling may occur at runtime.

            scheduler.Schedule<CronJob>("* * * * *");
            scheduler.Schedule<CronJob>("* * * * *", DateTimeOffset.UtcNow);

            using (LongJobs.Run(scheduler))
            {
                scheduler.Schedule<FireAndForgetJob>(DateTimeOffset.UtcNow);

                Thread.Sleep(1000);

                Assert.AreEqual(1, CronJob.Invokations);
                Assert.AreEqual(1, FireAndForgetJob.Invokations);
            }
        }

        [TestMethod]
        public void ShouldBeAbleToScheduleAndRunJobsViaAutofac()
        {
            var builder = new ContainerBuilder();

            builder.AddLongRunner();
            builder.RegisterLongJobs(typeof(LongJobsTests).Assembly);

            using (var container = builder.Build())
            {
                var scheduler = container.Resolve<ILongJobScheduler>();

                scheduler.Schedule<CronJob>("* * * * *");
                scheduler.Schedule<CronJob>("* * * * *", DateTimeOffset.UtcNow);
                scheduler.Schedule<FireAndForgetJob>(DateTimeOffset.UtcNow);

                Thread.Sleep(1000);

                Assert.AreEqual(1, CronJob.Invokations);
                Assert.AreEqual(1, FireAndForgetJob.Invokations);
            }
        }
    }
}
