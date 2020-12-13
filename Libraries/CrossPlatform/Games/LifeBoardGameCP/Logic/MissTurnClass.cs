using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using LifeBoardGameCP.Data;
using System.Threading.Tasks;
namespace LifeBoardGameCP.Logic
{
    [SingletonGame]
    public class MissTurnClass : IMissTurnClass<LifeBoardGamePlayerItem>
    {
        Task IMissTurnClass<LifeBoardGamePlayerItem>.PlayerMissTurnAsync(LifeBoardGamePlayerItem player)
        {
            ToastPlatform.ShowInfo($"{player.NickName} has lost this turn");
            return Task.CompletedTask;
        }
    }
}