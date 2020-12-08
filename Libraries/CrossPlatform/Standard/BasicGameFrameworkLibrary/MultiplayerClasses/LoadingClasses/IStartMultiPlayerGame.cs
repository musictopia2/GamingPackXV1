using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses
{
    public interface IStartMultiPlayerGame<P>
        where P : class, IPlayerItem, new()
    {
        Task LoadNewGameAsync(PlayerCollection<P> startList);
        Task LoadSavedGameAsync(); //this means there has to be saved data period.
    }
}