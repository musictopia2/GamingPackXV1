using BasicGameFrameworkLibrary.RegularDeckOfCards;
using System.Threading.Tasks;
namespace PickelCardGameCP.Logic
{
    public interface IBidProcesses
    {
        Task PassBidAsync();
        bool CanPass();
        Task ProcessBidAsync();
        void ResetBids();
        Task PopulateBidsAsync();
        void SelectBidAndSuit(int bid, EnumSuitList suit);
    }
}