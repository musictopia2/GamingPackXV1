using System.Threading.Tasks;
namespace PaydayCP.Logic
{
    public interface IBuyProcesses
    {
        Task BuyerSelectedAsync(int deck);
        Task ProcessBuyerAsync();
    }
}