using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using MillebournesCP.Cards;
namespace MillebournesCP.Data
{
    [SingletonGame]
    public class MillebournesSaveInfo : BasicSavedCardClass<MillebournesPlayerItem, MillebournesCardInformation>
    {
        public int LastThrowAway { get; set; }
        public int CurrentTeam { get; set; }
        public bool DidClone100 { get; set; }
        public CustomBasicList<TempData> TeamData { get; set; } = new CustomBasicList<TempData>();
    }
}