using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace ConnectTheDotsCP.Data
{
    [SingletonGame]
    public class ConnectTheDotsSaveInfo : BasicSavedGameClass<ConnectTheDotsPlayerItem>
    {
        public SavedBoardData? BoardData { get; set; }
    }
}