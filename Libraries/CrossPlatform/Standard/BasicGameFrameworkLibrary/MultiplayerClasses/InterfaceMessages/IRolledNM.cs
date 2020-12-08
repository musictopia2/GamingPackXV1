using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IRolledNM
    {
        Task RollReceivedAsync(string data);
    }
}