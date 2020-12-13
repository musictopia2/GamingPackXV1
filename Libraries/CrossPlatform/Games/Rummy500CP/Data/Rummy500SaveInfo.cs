using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace Rummy500CP.Data
{
    [SingletonGame]
    public class Rummy500SaveInfo : BasicSavedCardClass<Rummy500PlayerItem, RegularRummyCard>
    {
        public CustomBasicList<SavedSet> SetList { get; set; } = new CustomBasicList<SavedSet>();
        public DeckRegularDict<RegularRummyCard> DiscardList { get; set; } = new DeckRegularDict<RegularRummyCard>();
        public bool MoreThanOne { get; set; }
    }
}