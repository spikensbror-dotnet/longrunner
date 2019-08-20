using LongRunner;
using LongRunner.Autofac;
using System.Reflection;

namespace Autofac
{
    /// <summary>
    /// Extension methods for registering LongRunner with Autofac.
    /// </summary>
    public static class LongRunnerContainerBuilderExtensions
    {
        /// <summary>
        /// Adds a LongRunner scheduler to the specified builder and registers it
        /// for startup when the container is built.
        /// </summary>
        /// <param name="builder">The builder to add LongRunner to.</param>
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

        /// <summary>
        /// Registers <see cref="ILongJob"/> implementations in the given assemblies.
        /// </summary>
        /// <param name="builder">The builder to register fast jobs for.</param>
        /// <param name="assemblies">The assemblies to discover fast job implementations in.</param>
        public static void RegisterLongJobs(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.IsAssignableTo<ILongJob>())
                .AsSelf()
                .As<ILongJob>()
                .SingleInstance();
        }
    }
}
