using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface ITrickNM
    {
        Task TrickPlayReceivedAsync(int deck);
    }
}