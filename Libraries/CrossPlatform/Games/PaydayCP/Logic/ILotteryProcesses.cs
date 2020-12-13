using System.Threading.Tasks;
namespace PaydayCP.Logic
{
    public interface ILotteryProcesses
    {
        void LoadLotteryList();
        bool CanStartLotteryProcess();
        Task ProcessLotteryAsync();
        Task RollLotteryAsync();
    }
}