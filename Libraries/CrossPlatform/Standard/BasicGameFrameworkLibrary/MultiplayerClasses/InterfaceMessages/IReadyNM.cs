using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IReadyNM
    {
        Task ProcessReadyAsync(string nickName); //in this case, you will receive the nick name.
    }
}