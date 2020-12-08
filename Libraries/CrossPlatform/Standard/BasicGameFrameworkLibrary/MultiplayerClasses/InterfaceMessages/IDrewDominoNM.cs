using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IDrewDominoNM
    {
        Task DrewDominoReceivedAsync(int deck);
    }
}