using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace MonasteryCardGameCP.Data
{
    [SingletonGame]
    public class MonasteryCardGameSaveInfo : BasicSavedCardClass<MonasteryCardGamePlayerItem, MonasteryCardInfo>
    {
        public CustomBasicList<SavedSet> SetList { get; set; } = new CustomBasicList<SavedSet>();
        public int Mission { get; set; }
    }
}