using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using BasicGameFrameworkLibrary.CommandClasses;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.StockClasses
{
    public abstract class StockPileVM<D> : ObservableObject
        where D : IDeckObject, new()
    {
        public event StockClickedEventHandler? StockClickedAsync;
        public delegate Task StockClickedEventHandler();
        public BasicMultiplePilesCP<D> StockFrame;
        public void ClearCards()
        {
            StockFrame.PileList.Single().ObjectList.Clear(); // i think
        }
        public void AddCard(D thisCard)
        {
            StockFrame.PileList.Single().ObjectList.Add(thisCard);
        }
        public D GetCard()
        {
            return StockFrame.PileList.Single().ObjectList.Last(); // because that is what the ui shows.  this means you do in opposite order.
        }
        public int CardSelected()
        {
            if (StockFrame.PileList.Single().ObjectList.Count == 0)
            {
                return 0;
            }
            var thisCard = GetCard();
            if (thisCard.IsSelected == false)
            {
                return 0;
            }
            return thisCard.Deck;
        }
        public DeckRegularDict<D> GetStockList() // try list of.  if i need observable, can do as well
        {
            return StockFrame.PileList.Single().ObjectList.ToRegularDeckDict();
        }
        public void RemoveCard()
        {
            StockFrame.PileList.Single().ObjectList.RemoveLastItem(); // has to get rid of last item because of how the piles work.
            StockFrame.PileList.Single().IsSelected = false; //just in case.
            if (StockFrame.PileList.Single().ObjectList.Count > 0)
            {
                var thisCard = GetCard();
                thisCard.IsUnknown = false; // to guarantee no matter what,.
            }
        }
        public StockPileVM(CommandContainer command)
        {
            StockFrame = new BasicMultiplePilesCP<D>(command);
            StockFrame.Style = EnumMultiplePilesStyleList.HasList; // for sure has a list
            StockFrame.Rows = 1;
            StockFrame.Columns = 1;
            StockFrame.HasText = true;
            StockFrame.HasFrame = true;
            StockFrame.LoadBoard();
            StockFrame.PileList.Single().Text = TextToAppear;
            StockFrame.PileClickedAsync += StockFrame_PileClickedAsync;
        }
        public void UnselectCard()
        {
            if (StockFrame.PileList.First().ObjectList.Count == 0)
            {
                return;
            }
            var thisCard = GetCard();
            thisCard.IsSelected = false;
            StockFrame.PileList.Single().IsSelected = false; //i think this too.
        }
        public void SelectCard()
        {
            var thisCard = GetCard();
            thisCard.IsSelected = true;
            StockFrame.PileList.Single().IsSelected = true; //i think this too now to be compatible with blazor.
        }
        public bool DidGoOut()
        {
            if (StockFrame.PileList.Single().ObjectList.Count == 0)
            {
                return true;
            }
            return false;
        }
        public int CardsLeft()
        {
            return StockFrame.PileList.Single().ObjectList.Count;
        }
        public abstract string NextCardInStock(); // could be W even
        private async Task StockFrame_PileClickedAsync(int index, BasicPileInfo<D> thisPile)
        {
            if (StockClickedAsync == null)
            {
                return;
            }
            await StockClickedAsync.Invoke();
        }
        protected virtual string TextToAppear
        {
            get
            {
                return "Stock";
            }
        }
    }
}