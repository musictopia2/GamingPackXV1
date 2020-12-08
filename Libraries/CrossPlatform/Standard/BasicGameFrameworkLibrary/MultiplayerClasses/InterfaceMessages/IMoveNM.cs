using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IMoveNM
    {
        Task MoveReceivedAsync(string data);
    }
}