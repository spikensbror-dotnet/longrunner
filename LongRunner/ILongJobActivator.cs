using System;

namespace LongRunner
{
    public interface ILongJobActivator
    {
        ILongJob Activate(Type jobType);
    }
}
