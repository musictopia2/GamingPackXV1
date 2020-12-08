using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.MultiplePilesObservable
{
    public partial class BasicMultiplePilesCP<D> : SimpleControlObservable where D : IDeckObject, new()
    {
        private EnumMultiplePilesStyleList _style = EnumMultiplePilesStyleList.None;
        public EnumMultiplePilesStyleList Style
        {
            get
            {
                return _style;
            }
            set
            {
                if (SetProperty(ref _style, value) == true) { }
            }
        }
        private int _rows;
        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                if (SetProperty(ref _rows, value) == true) { }
            }
        }
        private int _columns;
        public int Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                if (SetProperty(ref _columns, value) == true) { }
            }
        }
        private bool _hasFrame;
        public bool HasFrame
        {
            get
            {
                return _hasFrame;
            }
            set
            {
                if (SetProperty(ref _hasFrame, value) == true) { }
            }
        }
        private bool _hasText = true;
        public bool HasText
        {
            get
            {
                return _hasText;
            }
            set
            {
                if (SetProperty(ref _hasText, value) == true) { }
            }
        }
        private int _discardsRemoved;
        private void CheckErrors(string section)
        {
            if (Style == EnumMultiplePilesStyleList.None)
            {
                throw new BasicBlankException("Must have a style chosen.  The section is " + section);
            }
            if (PileList!.Count + _discardsRemoved != Rows * Columns)
            {
                throw new BasicBlankException("Since there is at least one item in the pilelist; then the number must match rows times columns.  The section is " + section);
            }
            if (HasFrame == false & HasText == true)
            {
                throw new BasicBlankException("Cannot allow text because hasframe is false.  The section is " + section);
            }
            if (Rows == 0 | Columns == 0)
            {
                throw new BasicBlankException("The rows and columns must be greater than 0.  The section is " + section);
            }
            foreach (var thisPile in PileList)
            {
                if (thisPile.Text != "" & HasText == false)
                {
                    throw new BasicBlankException("Cannot have text because HasText is false.  The section is " + section + ".  The Text Was " + thisPile.Text);
                }
                if (thisPile.Text != "" & HasFrame == false)
                {
                    throw new BasicBlankException("Cannot have text because HasFrame is false.  The section is " + section);
                }
                if (thisPile.Text == "" & HasText == true)
                {
                    throw new BasicBlankException("Must have text entered.  The section is " + section);
                }
                if (Style == EnumMultiplePilesStyleList.HasList & thisPile.ThisObject.Deck > 0)
                {
                    throw new BasicBlankException("Since this is a list; the individual card must be blank.  The section is " + section);
                }
                if (Style == EnumMultiplePilesStyleList.SingleCard & thisPile.ObjectList.Count > 0)
                {
                    throw new BasicBlankException("Since this is single card only; cannot have any items on the cardlist.  The section is " + section);
                }
            }
        }
        public CustomBasicList<BasicPileInfo<D>>? PileList; //decided to not set to begin with so i have to call loadboard.
        public ControlCommand PileCommand { get; set; }

        public ControlCommand? ExtraCommand; //will not set this.  if set, can enable too.  this will allow this to be notified when enable change.
        protected virtual async Task OnPileClickedAsync(int index, BasicPileInfo<D> thisPile)
        {
            if (thisPile.IsEnabled == false)
            {
                return; //since tablets sometimes has the problem, this provides a way to catch so it still won't run after all.
            }
            if (PileClickedAsync == null)
            {
                return;
            }
            await PileClickedAsync.Invoke(index, thisPile);
        }

        private async Task PrivatePileClickedAsync(BasicPileInfo<D> pile)
        {
            await OnPileClickedAsync(PileList!.IndexOf(pile), pile);
        }

        public event PileClickedEventHandler? PileClickedAsync; // sometimes, this Is needed
        public delegate Task PileClickedEventHandler(int index, BasicPileInfo<D> thisPile);
        public BasicMultiplePilesCP(CommandContainer command) : base(command)
        {
            MethodInfo method = this.GetPrivateMethod(nameof(PrivatePileClickedAsync));
            PileCommand = new ControlCommand(this, method, command);
        }
        public void RemoveFirstDiscardPiles(int howMany)
        {
            _discardsRemoved = howMany;
            if (PileList!.Count == 0)
            {
                throw new BasicBlankException("Must have piles already to remove the first piles");
            }
            if (PileList.Count <= howMany)
            {
                throw new BasicBlankException("Must have more than " + howMany + " in order to remove the first discard piles");
            }
            PileList.RemoveRange(0, howMany);
        }
        public void RemoveLastDiscardPiles(int howMany)
        {
            _discardsRemoved = howMany;
            if (PileList!.Count == 0)
            {
                throw new BasicBlankException("Must have piles already to remove the first piles");
            }
            if (PileList.Count <= howMany)
            {
                throw new BasicBlankException("Must have more than " + howMany + " in order to remove the first discard piles");
            }
            var startAt = PileList.Count - howMany;  // iffy.
            PileList.RemoveRange(startAt, howMany); // i think
        }
        public void RemoveSpecificDiscardPiles(CustomBasicList<int> indexList)
        {
            var tempList = new CustomBasicList<BasicPileInfo<D>>();
            indexList.ForEach(index =>
            {
                tempList.Add(PileList![index]);
            });
            tempList.ForEach(thisPile =>
            {
                PileList!.RemoveSpecificItem(thisPile);
            });
            _discardsRemoved = indexList.Count;
        }
        public void LoadBoard()
        {
            if (Rows == 0 || Columns == 0)
            {
                throw new BasicBlankException("Must have at least one row and one column");
            }
            int x;
            int y;
            PileList = new CustomBasicList<BasicPileInfo<D>>();
            BasicPileInfo<D> thisPile;
            var loopTo = Rows;
            for (x = 1; x <= loopTo; x++)
            {
                var loopTo1 = Columns;
                for (y = 1; y <= loopTo1; y++)
                {
                    thisPile = new BasicPileInfo<D>();
                    thisPile.Column = y;
                    thisPile.Row = x;
                    PileList.Add(thisPile);
                }
            }
            if (PileList.Count == 0)
            {
                throw new Exception("No piles");
            }
        }
        public void SelectUnselectSinglePile(int index)
        {
            BasicPileInfo<D> thisPile;
            thisPile = PileList![index];
            if (thisPile.Visible == false)
            {
                throw new BasicBlankException("Sorry; the pile " + index + " is not visible");
            }

            if (thisPile.IsSelected == true)
            {
                thisPile.IsSelected = false;
                return;
            }
            foreach (var temps in PileList)
            {
                temps.IsSelected = false;
            }
            thisPile = PileList[index];
            thisPile.IsSelected = true;
        }
        public virtual void SelectPile(int index)
        {
            BasicPileInfo<D> thisPile;
            thisPile = PileList![index];
            if (thisPile.Visible == false)
            {
                throw new BasicBlankException("Sorry; the pile " + index + " is not visible");
            }
            thisPile.IsSelected = true;
        }
        public void UnselectPile(int index)
        {
            BasicPileInfo<D> thisPile;
            thisPile = PileList![index];
            if (thisPile.Visible == false)
            {
                throw new BasicBlankException("Sorry; the pile " + index + " is not visible");
            }
            thisPile.IsSelected = false;
        }
        public int SinglePileSelected()
        {
            var tempList = from piles in PileList
                           where piles.IsSelected == true
                           select piles;
            if (tempList.Count() == 0)
            {
                return -1;
            }
            if (tempList.Count() > 1)
            {
                throw new Exception("There is more than one selected.  Therefore; cannot figure out the single pile selected");
            }
            return PileList!.LastIndexOf(tempList.ElementAt(0));
        }
        public virtual void AddCardToPile(int pile, D thisCard)
        {
            BasicPileInfo<D> thisPile;
            thisCard.IsSelected = false;
            thisCard.Drew = false;
            thisPile = PileList![pile];
            thisCard.Rotated = thisPile.Rotated;
            if (Style == EnumMultiplePilesStyleList.SingleCard)
            {
                thisPile.ThisObject = thisCard;
            }
            else
            {
                thisPile.ObjectList.Add(thisCard);
            }
        }
        protected virtual void PossiblePileChangeWhenClearingBoard(BasicPileInfo<D> thisPile) { }
        protected virtual bool CanClearCardsAutomatically()
        {
            return true;
        }
        public virtual void ClearBoard() // games like concentration will have their own implementation.
        {
            CheckErrors("ClearBoard");
            foreach (var thisPile in PileList!)
            {
                PossiblePileChangeWhenClearingBoard(thisPile);
                if (Style == EnumMultiplePilesStyleList.HasList)
                {
                    if (CanClearCardsAutomatically() == true)
                    {
                        thisPile.ObjectList.Clear();
                    }
                    thisPile.TempList.Clear();
                }
                else
                {
                    thisPile.ThisObject = new D();
                }
                thisPile.IsSelected = false;
            }
        }
        //we no longer need the newcardprocesses anymore thanks to blazor programming model.
        
        public void MoveSingleCard(int previousPile, int newPile)
        {
            D thisCard;
            thisCard = GetLastCard(previousPile);
            RemoveCardFromPile(previousPile);
            AddCardToPile(newPile, thisCard);
        }

        public virtual void RemoveCardFromPile(int index) //so games like heap solitaire can do something else in addition.
        {
            BasicPileInfo<D> thisPile;
            thisPile = PileList![index];
            if (thisPile.Visible == false)
            {
                throw new BasicBlankException("Sorry; the pile " + index + " is not visible");
            }
            thisPile.IsSelected = false;
            if (Style == EnumMultiplePilesStyleList.HasList)
            {
                if (thisPile.ObjectList.Count == 0)
                {
                    throw new BasicBlankException("Cannot remove card because there are no cards left");
                }
                thisPile.ObjectList.RemoveLastItem();
                AfterRemoveCardFromPile(thisPile);
                return;
            }
            if (thisPile.ThisObject.Deck == 0)
            {
                throw new BasicBlankException("The card is already blank.  Therefore cannot remove");
            }
            thisPile.ThisObject = new D();
        }
        protected virtual void AfterRemoveCardFromPile(BasicPileInfo<D> thisPile) { }
        public bool HasCard(int index)
        {
            D card;
            card = GetLastCard(index);
            if (card.Deck == 0)
            {
                return false;
            }
            return true;
        }
        public D GetLastCard(BasicPileInfo<D> thisPile) // i may need to know the last card afterall.
        {
            CheckErrors("GetLastCard");
            if (thisPile.Visible == false)
            {
                throw new Exception("Sorry; the pile is not visible");
            }
            if (Style == EnumMultiplePilesStyleList.SingleCard)
            {
                return thisPile.ThisObject;
            }
            if (thisPile.ObjectList.Count == 0)
            {
                D thisC = new D();
                thisC.Rotated = thisPile.Rotated;
                return thisC;
            }
            return thisPile.ObjectList.Last();
        }
        protected virtual bool CanAutoUnselect()
        {
            return true;
        }
        public D GetLastCard(int index)
        {
            BasicPileInfo<D> thisPile;
            thisPile = PileList![index];
            if (thisPile.Visible == false)
            {
                throw new BasicBlankException("Sorry; the pile " + index + " is not visible");
            }
            D tempCard;
            tempCard = GetLastCard(thisPile);
            if (CanAutoUnselect() == true)
            {
                tempCard.IsSelected = false;
            }
            return tempCard;
        }
        public DeckRegularDict<D> GetCardList()
        {
            DeckRegularDict<D> output = new DeckRegularDict<D>();
            int x = 10000; //i doubt we would have that many cards.
            PileList!.ForEach(thisPile =>
            {
                if (Style == EnumMultiplePilesStyleList.SingleCard)
                {
                    output.Add(thisPile.ThisObject);
                }
                else if (thisPile.ObjectList.Count > 0)
                {
                    output.Add(GetLastCard(thisPile));
                }
                else
                {
                    x++; //because of the dictionaries work.
                    D thisD = new D();
                    thisD.Deck = x; //because of how dictionaries work.
                    output.Add(thisD); //games like bisley would not work otherwise.
                }
            });
            return output;
        }
        protected override void EnableChange()
        {
            PileCommand.ReportCanExecuteChange(); //you do need this though.
            if (ExtraCommand != null)
            {
                ExtraCommand.ReportCanExecuteChange(); //this is used for games like millebournes where you don't inherit from this.
            }
        }
        protected override void PrivateEnableAlways() { }
    }
}