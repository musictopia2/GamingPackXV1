using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IProcessHoldNM
    {
        Task ProcessHoldReceivedAsync(int iD); //this for sure is an integer.
    }
}