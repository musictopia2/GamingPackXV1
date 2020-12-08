using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IPlayDominoNM
    {
        Task PlayDominoMessageAsync(int deck); //needs to be integer for sure.  used for domino games.
    }
}