using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IPickUpNM
    {
        Task PickUpReceivedAsync(string data);
    }
}