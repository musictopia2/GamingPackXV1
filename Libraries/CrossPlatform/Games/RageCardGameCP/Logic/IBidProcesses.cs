using System.Threading.Tasks;
namespace RageCardGameCP.Logic
{
    public interface IBidProcesses
    {
        Task ProcessBidAsync();
        Task LoadBiddingScreenAsync();
    }
}