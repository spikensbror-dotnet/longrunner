using System.Threading.Tasks;

namespace LongRunner
{
    public interface ILongJob
    {
        Task Execute();
    }
}
