using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.Dice
{
    public interface IRollMultipleDice<T> : IAdvancedDIContainer  //i think it needs to take in a type.
    {
        Task ShowRollingAsync(CustomBasicList<CustomBasicList<T>> thisCol);
        Task SendMessageAsync(string category, CustomBasicList<CustomBasicList<T>> thisList); //decided to not send anything now.
        Task<CustomBasicList<CustomBasicList<T>>> GetDiceList(string content); //could send in a delegate but for now, don't worry about it.
        CustomBasicList<CustomBasicList<T>> RollDice(int howManySections = 6); //you do have to decide how many times it will do this.
    }
}