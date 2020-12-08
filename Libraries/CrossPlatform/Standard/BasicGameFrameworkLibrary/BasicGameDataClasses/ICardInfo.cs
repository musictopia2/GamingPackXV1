using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.BasicGameDataClasses
{
    /// <summary>
    /// this is used for card games.  this is all the data that has to be populated to change behaviors based on information.
    /// </summary>
    public interface ICardInfo<D> where D : IDeckObject, new()
    {
        int CardsToPassOut { get; }
        CustomBasicList<int> DiscardExcludeList(IListShuffler<D> deckList); //try this way.
        CustomBasicList<int> PlayerExcludeList { get; } //this lists the cards that can't be in the players hand to begin with and maybe not even face up for discard.
        bool AddToDiscardAtBeginning { get; }
        bool ReshuffleAllCardsFromDiscard { get; }
        bool ShowMessageWhenReshuffling { get; } //i think read only
        bool PassOutAll { get; }
        bool PlayerGetsCards { get; }
        bool NoPass { get; }
        bool NeedsDummyHand { get; }
        DeckRegularDict<D> DummyHand { get; set; } //unfortunately, i need it after all.  because this is used when passing out cards.
        bool HasDrawAnimation { get; } //can have a class for default stuff.  but you can adjust as needed.
        bool CanSortCardsToBeginWith { get; } //some games can't sort cards to begin with.
    }
}