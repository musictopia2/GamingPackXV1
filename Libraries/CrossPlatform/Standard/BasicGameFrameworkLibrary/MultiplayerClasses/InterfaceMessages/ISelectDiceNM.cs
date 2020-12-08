using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface ISelectDiceNM
    {
        Task SelectDiceReceivedAsync(int iD);
    }
}