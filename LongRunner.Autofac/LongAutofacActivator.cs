using Autofac;
using System;

namespace LongRunner.Autofac
{
    class LongAutofacActivator : ILongJobActivator
    {
        private readonly IComponentContext componentContext;

        public LongAutofacActivator(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public ILongJob Activate(Type jobType)
        {
            return (ILongJob)this.componentContext.Resolve(jobType);
        }
    }
}
