using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using FluxxCP.Cards;
using System.Threading.Tasks;
namespace FluxxCP.Logic
{
    public interface IEmptyTrashProcesses
    {
        Task EmptyTrashAsync();
        Task FinishEmptyTrashAsync(IEnumerableDeck<FluxxCardInformation> cardList);
    }
}