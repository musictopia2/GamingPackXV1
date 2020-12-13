using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using System.Threading.Tasks;
namespace GolfCardGameCP.Logic
{
    public interface IBeginningProcesses
    {
        Task SelectBeginningAsync(int player, DeckRegularDict<RegularSimpleCard> selectList, DeckRegularDict<RegularSimpleCard> unselectList);
    }
}