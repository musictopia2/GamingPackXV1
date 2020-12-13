using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace ConnectFourCP.Data
{
    [SingletonGame]
    public class ConnectFourSaveInfo : BasicSavedGameClass<ConnectFourPlayerItem>
    {
        public ConnectFourCollection GameBoard { get; set; } = new ConnectFourCollection();
    }
}