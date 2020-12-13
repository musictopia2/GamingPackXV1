using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace CousinRummyCP.Data
{
    [SingletonGame]
    public class CousinRummySaveInfo : BasicSavedCardClass<CousinRummyPlayerItem, RegularRummyCard>
    {
        public CustomBasicList<SavedSet> SetList { get; set; } = new CustomBasicList<SavedSet>();
        public int Round { get; set; }
        public int WhoDiscarded { get; set; } //0 means nobody.
    }
}