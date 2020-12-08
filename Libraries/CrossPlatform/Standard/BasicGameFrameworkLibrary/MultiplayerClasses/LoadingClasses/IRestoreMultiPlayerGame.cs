using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses
{
    /// <summary>
    /// this is used in cases where you are restoring a saved game.
    /// </summary>
    public interface IRestoreMultiPlayerGame
    {
        /// <summary>
        /// this restores the game back to where it was when you first started playing it.
        /// only works if you set the save type to restore only.  otherwise, it will not work.
        /// if that changes, rethink.
        /// </summary>
        /// <returns></returns>
        Task RestoreGameAsync(); //this is all it needs.
    }
}