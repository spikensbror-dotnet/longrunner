using LongRunner;
using LongRunner.Autofac;

namespace Autofac
{
    public static class LongRunnerContainerBuilderExtensions
    {
        public static void AddLongRunner(this ContainerBuilder builder)
        {
            builder.Register(c => LongJobs.CreateScheduler(new LongAutofacActivator(c.Resolve<ILifetimeScope>())))
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<JobStartup>()
                .AsSelf()
                .SingleInstance();

            // Resolve JobStartup so that it lives along with the container.
            builder.RegisterBuildCallback(c => c.Resolve<JobStartup>());
        }
    }
}
