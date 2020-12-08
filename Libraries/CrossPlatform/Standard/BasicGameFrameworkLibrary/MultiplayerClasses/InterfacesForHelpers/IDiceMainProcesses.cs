using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers
{
    public interface IDiceMainProcesses<P> : IBasicGameProcesses<P>
        where P : class, IPlayerItem, new()
    {
        Task HoldUnholdDiceAsync(int index);
    }
}