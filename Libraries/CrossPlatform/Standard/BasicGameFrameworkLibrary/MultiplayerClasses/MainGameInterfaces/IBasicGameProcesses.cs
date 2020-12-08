using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces
{
    public interface IBasicGameProcesses<P> : IAggregatorContainer
        where P : class, IPlayerItem, new()
    {
        PlayerCollection<P> PlayerList { get; set; } //found more than one use for it now.
        P? SingleInfo { get; set; }
        BasicData BasicData { get; }
        INetworkMessages? Network { get; set; }
        IMessageChecker? Check { get; set; }
        IViewModelData CurrentMod { get; }
        bool CanMakeMainOptionsVisibleAtBeginning { get; } //for card games, default to true.  but can make it overridable.
    }
}