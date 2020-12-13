using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace ConcentrationCP.Data
{
    [SingletonGame]
    public class ConcentrationSaveInfo : BasicSavedCardClass<ConcentrationPlayerItem, RegularSimpleCard>
    {
        public CustomBasicList<BasicPileInfo<RegularSimpleCard>> BoardList { get; set; } = new CustomBasicList<BasicPileInfo<RegularSimpleCard>>();
        public DeckRegularDict<RegularSimpleCard> ComputerList { get; set; } = new DeckRegularDict<RegularSimpleCard>();
    }
}