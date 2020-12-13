using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace MancalaCP.Data
{
    [SingletonGame]
    public class MancalaSaveInfo : BasicSavedGameClass<MancalaPlayerItem>
    {
        public bool IsStart { get; set; }
    }
}