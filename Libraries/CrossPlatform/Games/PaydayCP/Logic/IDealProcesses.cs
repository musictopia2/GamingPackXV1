using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using PaydayCP.Cards;
using System.Threading.Tasks;
namespace PaydayCP.Logic
{
    public interface IDealProcesses
    {
        Task ChoseWhetherToPurchaseDealAsync();
        Task ProcessDealAsync(bool isYardSale);
        void SetUpDeal();
        void PopulateDeals();
        Task ContinueDealProcessesAsync();
        Task ReshuffleDealsAsync(DeckRegularDict<DealCard> list);
        Task<bool> ProcessShuffleDealsAsync();
    }
}