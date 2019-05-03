using Autofac;
using System;

namespace LongRunner.ExampleApp
{
    static class AutofacProgram
    {
        public static void Run(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleWriteTestJob>();
            builder.AddLongRunner();

            var otherBuilder = new ContainerBuilder();
            otherBuilder.RegisterType<OtherConsoleWriteTestJob>();
            otherBuilder.AddLongRunner();

            using (var container = builder.Build())
            using (var otherContainer = otherBuilder.Build())
            {
                // Every minute.
                container.Resolve<ILongJobScheduler>().Schedule<ConsoleWriteTestJob>("* * * * *");

                // Every five minutes.
                otherContainer.Resolve<ILongJobScheduler>().Schedule<OtherConsoleWriteTestJob>("0 0/5 * 1/1 * ? *");

                Console.ReadKey(true);
            }
        }
    }
}
