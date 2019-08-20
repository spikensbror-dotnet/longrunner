using System.Threading.Tasks;

namespace LongRunner
{
    /// <summary>
    /// A job to be scheduled by LongRunner.
    /// </summary>
    public interface ILongJob
    {
        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <returns>A task that completes once the job is completed.</returns>
        Task Execute();
    }
}
