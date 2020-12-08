using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.EventModels
{
    /// <summary>
    /// whoever is responsible for this event needs to do something with this.  its only used when its the first time.
    /// if its a brand new game, something else gets triggered.
    /// </summary>
    /// <typeparam name="P"></typeparam>
    public class StartMultiplayerGameEventModel<P>
        where P : class, IPlayerItem, new()
    {
        public PlayerCollection<P> PlayerList;
        public StartMultiplayerGameEventModel(PlayerCollection<P> playerList)
        {
            PlayerList = playerList;
        }
    }
}