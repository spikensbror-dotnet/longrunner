using System;

namespace LongRunner
{
    class DefaultJobActivator : ILongJobActivator
    {
        public ILongJob Activate(Type jobType)
        {
            return (ILongJob)Activator.CreateInstance(jobType);
        }
    }
}
