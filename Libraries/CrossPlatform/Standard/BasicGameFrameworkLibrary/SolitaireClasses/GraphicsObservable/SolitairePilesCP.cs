using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings;
namespace BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable
{
    public class SolitairePilesCP : ObservableObject
    {
        #region "Variables"
        private readonly DeckRegularDict<SolitaireCard> _listUnknownCards = new DeckRegularDict<SolitaireCard>();
        public CustomBasicList<PileInfoCP> PileList = new CustomBasicList<PileInfoCP>();
        #endregion
        #region "Properties"
        /// <summary>
        /// games like persian solitaire needs this to set the proper key.
        /// </summary>
        public static int DealNumber { get; set; }
        private bool _isKlondike = false;
        public bool IsKlondike
        {
            get { return _isKlondike; }
            set
            {
                if (SetProperty(ref _isKlondike, value)) { }
            }
        }
        private int _numberOfPiles;
        public int NumberOfPiles
        {
            get { return _numberOfPiles; }
            set
            {
                if (SetProperty(ref _numberOfPiles, value)) { }
            }
        }
        #endregion
        #region "Events/Commands"
        public event WastePileSelectedEventHandler? ColumnClickedAsync;
        public event WasteDoubleClickEventHandler? DoubleClickedAsync;
        public PlainCommand ColumnCommand { get; set; }
        public PlainCommand DoubleCommand { get; set; }

        private async Task PrivateColumnAsync(PileInfoCP pile)
        {
            if (ColumnClickedAsync == null)
            {
                return;
            }
            await ColumnClickedAsync.Invoke(PileList.IndexOf(pile));
        }
        private async Task PrivateDoubleAsync(PileInfoCP pile)
        {
            if (DoubleClickedAsync == null)
            {
                return;
            }
            await DoubleClickedAsync.Invoke(PileList.IndexOf(pile));
        }

        public SolitairePilesCP(CommandContainer command)
        {
            MethodInfo method = this.GetPrivateMethod(nameof(PrivateColumnAsync));
            ColumnCommand = new PlainCommand(this, method, canExecute: null, null!, command);
            method = this.GetPrivateMethod(nameof(PrivateDoubleAsync));
            DoubleCommand = new PlainCommand(this, method, canExecute: null, null!, command);
        }
        #endregion
        #region "Methods/Functions"
        public void SelectUnselectPile(int pile)
        {
            var thisPile = PileList[pile];
            if (thisPile.IsSelected)
            {
                thisPile.IsSelected = false;
                return;
            }
            PileList.ForEach(tempPile => tempPile.IsSelected = false);
            thisPile.IsSelected = true;
        }
        public int SinglePileSelected()
        {
            int counts = PileList.Count(items => items.IsSelected);
            if (counts == 0)
            {
                return -1;
            }
            if (counts > 1)
            {
                throw new BasicBlankException("More than one pile is selected.  Find out what happened");
            }
            var tempPile = PileList.Single(items => items.IsSelected);
            return PileList.IndexOf(tempPile);
        }
        public DeckRegularDict<SolitaireCard> ListGivenCards(int pile)
        {
            var thisPile = PileList[pile];
            DeckRegularDict<SolitaireCard> output = new DeckRegularDict<SolitaireCard>();
            var thisTemp = thisPile.CardList.ToRegularDeckDict();
            thisTemp.Reverse();
            foreach (var thisCard in thisTemp)
            {
                if (thisCard.IsUnknown == false)
                {
                    output.Add(thisCard);
                }
                else
                {
                    break;
                }
            }
            output.Reverse(); //i think this is needed too.
            return output;
        }
        public virtual SolitaireCard GetLastCard(int column)
        {
            if (PileList[column].CardList.Count == 0)
            {
                throw new BasicBlankException("There are no cards to get");
            }
            return PileList[column].CardList.Last();
        }
        public virtual void RemoveCardFromColumn(int index)
        {
            if (PileList[index].CardList.Count == 0)
            {
                throw new BasicBlankException("There is no card to remove from column");
            }
            PileList[index].CardList.RemoveLastItem();
        }
        public void UnselectAllPiles()
        {
            PileList.ForEach(thisPile => thisPile.IsSelected = false);
        }
        public virtual void MoveSingleCard(int previousPile, int newPile)
        {
            var thisCard = GetLastCard(previousPile);
            RemoveCardFromColumn(previousPile);
            var thisPile = PileList[newPile];
            thisPile.CardList.Add(thisCard);
            UnselectAllPiles();
        }
        public void RemoveSpecificCardFromColumn(int pile, int deck)
        {
            PileList[pile].CardList.RemoveObjectByDeck(deck); //try without try/catch
        }
        public void RemoveFromUnknown(SolitaireCard thisCard)
        {
            _listUnknownCards.RemoveSpecificItem(thisCard); //i think.
            thisCard.IsUnknown = false;
        }
        public virtual void AddCardToColumn(int column, SolitaireCard thisCard)
        {
            PileList[column].CardList.Add(thisCard);
        }
        public bool HasCardInColumn(int column)
        {
            return PileList[column].CardList.Count > 0;
        }
        public async Task<string> GetSavedGameAsync()
        {
            string output = await js.SerializeObjectAsync(PileList);
            return output;
        }
        public async Task LoadSavedGameAsync(string data) //has to be this way because can be one of 2 types of piles with different structures.
        {
            PileList = await js.DeserializeObjectAsync<CustomBasicList<PileInfoCP>>(data);
        }
        public virtual void LoadBoard()
        {
            if (NumberOfPiles == 0)
            {
                throw new BasicBlankException("Must have at least 1 pile");
            }
            NumberOfPiles.Times(x =>
            {
                PileInfoCP thisPile = new PileInfoCP();
                PileList.Add(thisPile);
            });
        }
        public virtual void ClearBoard()
        {
            _listUnknownCards.Clear(); //try this instead.  hopefully still okay.
            PileList.ForEach(thisPile =>
            {
                thisPile.CardList.Clear();
                thisPile.TempList.Clear(); //i think this is needed too.
                thisPile.IsSelected = false;
            });
        }
        public void GetUnknownList() //run only once.
        {
            _listUnknownCards.Clear();
            PileList.ForEach(thisPile =>
            {
                thisPile.CardList.ForConditionalItems(items => items.IsUnknown == true, thisCard => _listUnknownCards.Add(thisCard));
            });
        }
        public void MakeUnknown()
        {
            if (_listUnknownCards.Count == 0)
            {
                return;
            }
            _listUnknownCards.ForEach(thisCard => thisCard.IsUnknown = true);
        }
        #endregion
    }
}