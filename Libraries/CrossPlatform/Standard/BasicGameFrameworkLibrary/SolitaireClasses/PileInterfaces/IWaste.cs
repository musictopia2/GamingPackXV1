using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SolitaireClasses.PileInterfaces
{
    public interface IWaste
    {
        void FirstLoad(bool isKlondike, IDeckDict<SolitaireCard> cardList);
        void FirstLoad(int rows, int columns);
        void ClearBoard(IDeckDict<SolitaireCard> thisCol);
        void GetUnknowns();
        bool CanAddSingleCard(int WhichOne, SolitaireCard thisCard);
        int CardsNeededToBegin { get; set; }
        bool CanMoveCards(int whichOne, out int lastOne);
        void MoveCards(int whichOne, int lasts);
        bool CanMoveToAnotherPile(int whichOne);
        void MoveSingleCard(int whichOne);
        IDeckDict<SolitaireCard> GetAllCards();
        event WastePileSelectedEventHandler PileSelectedAsync;
        event WasteDoubleClickEventHandler DoubleClickAsync;
        int HowManyPiles { get; set; }
        Task<SavedWaste> GetSavedGameAsync();
        Task LoadGameAsync(SavedWaste gameData);
        int OneSelected();
        void UnselectAllColumns();
        void SelectUnselectPile(int whichOne);
        bool HasCard(int whichOne);
        SolitaireCard GetCard();
        bool CanSelectUnselectPile(int whichOne);
        void DoubleClickColumn(int index);
        void RemoveSingleCard();
        void AddSingleCard(int whichOne, SolitaireCard thisCard);
        SolitaireCard GetCard(int whichOne); // better have too much than not enough.  because otherwise; i have to recompile again
    }
}