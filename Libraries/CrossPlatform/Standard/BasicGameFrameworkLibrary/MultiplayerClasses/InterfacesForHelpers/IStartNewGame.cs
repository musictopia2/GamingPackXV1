using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers
{
    /// <summary>
    /// this interface is needed in cases where the game has rounds and a person chose new game.
    /// i think every game needs something to reset because its truly different for every game.
    /// if a game has nothing, then just do nothing then.
    /// looks like even with the new ways of doing things, still need this part.
    /// </summary>
    public interface IStartNewGame
    {
        Task ResetAsync();
    }
}