using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public class BasicSavedCardClass<P, D> : BasicSavedGameClass<P>
        where P : class, IPlayerItem, new()
        where D : IDeckObject, new()
    {
        public SavedDiscardPile<D>? PublicDiscardData { get; set; }
        public CustomBasicList<int> PublicDeckList { get; set; } = new CustomBasicList<int>();//this is for the deck (cards needing to be drawn)
        public bool AlreadyDrew { get; set; }
        public int PreviousCard { get; set; }
        public int CurrentCard { get; set; }
    }
}