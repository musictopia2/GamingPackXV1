using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.Dice
{
    public interface IRollSingleDice<T> : IAdvancedDIContainer
    {
        Task ShowRollingAsync(CustomBasicList<T> thisCol);
        Task SendMessageAsync(string category, CustomBasicList<T> thisList);
        Task<CustomBasicList<T>> GetDiceList(string content);
        CustomBasicList<T> RollDice(int howManySections = 6);
    }
}