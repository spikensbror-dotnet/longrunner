using System;

namespace LongRunner
{
    /// <summary>
    /// Creates job instances.
    /// </summary>
    public interface ILongJobActivator
    {
        /// <summary>
        /// Creates an instance of the given job type.
        /// </summary>
        /// <param name="jobType">The type of job to create.</param>
        /// <returns>An instance of the job.</returns>
        ILongJob Activate(Type jobType);
    }
}
