using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MiscProcesses
{
    public interface IRetrieveSavedPlayers<P>
         where P : class, IPlayerItem, new()
    {
        Task<PlayerCollection<P>> GetPlayerListAsync(string payLoad);
    }
}