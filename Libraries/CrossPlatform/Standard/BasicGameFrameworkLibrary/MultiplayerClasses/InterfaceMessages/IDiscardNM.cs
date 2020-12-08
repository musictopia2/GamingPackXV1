using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IDiscardNM
    {
        Task DiscardReceivedAsync(string data);
    }
}
