using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IEndTurnNM
    {
        Task EndTurnReceivedAsync(string data); //sometimes there is data.  needs to keep the option open.
    }
}