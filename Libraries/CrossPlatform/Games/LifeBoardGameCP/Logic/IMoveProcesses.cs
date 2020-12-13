using System.Threading.Tasks;
namespace LifeBoardGameCP.Logic
{
    public interface IMoveProcesses
    {
        Task PossibleAutomateMoveAsync();
        Task DoAutomateMoveAsync(int space);
    }
}