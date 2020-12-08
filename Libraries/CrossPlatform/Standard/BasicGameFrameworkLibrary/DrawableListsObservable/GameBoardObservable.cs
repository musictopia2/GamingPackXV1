using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.DrawableListsObservable
{
    public abstract class GameBoardObservable<D> : ObservableObject, IControlObservable where D : IDeckObject, new()
    {
        public DeckRegularDict<D> ObjectList = new DeckRegularDict<D>();
        public int Columns { get; set; }
        public int Rows { get; set; }
        public int MaxCards { get; set; } = 0;
        private string _text = "";
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (SetProperty(ref _text, value) == true) { }
            }
        }
        private bool _hasFrame = true;
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
        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (SetProperty(ref _isEnabled, value) == true)
                {
                    ChangeEnabled();
                }
            }
        }
        private bool _visible = true; // since i already did the bindings there, has to be there instead.
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                if (SetProperty(ref _visible, value) == true) { }
            }
        }
        protected virtual void ChangeEnabled() { }
        public void LoadSavedGame(IDeckDict<D> list)
        {
            ObjectList.ReplaceRange(list); //risk a breaking change here. so i don't have to use strings anymore for this.
        }
        public int CalculateMaxCards()
        {
            if (MaxCards == 0)
            {
                return Columns * Rows;
            }
            return MaxCards;
        }
        protected D GetObject(int row, int column)
        {
            int x;
            int y;
            int z = 0;
            var loopTo = Rows;
            for (x = 1; x <= loopTo; x++)
            {
                var loopTo1 = Columns;
                for (y = 1; y <= loopTo1; y++)
                {
                    if (z + 1 > ObjectList.Count)
                    {
                        throw new BasicBlankException($"Somehow was out of range.  z was {z + 1} and count was {ObjectList.Count}");
                    }
                    if (y == column && x == row)
                    {
                        return ObjectList[z];
                    }
                    z += 1;
                }
            }
            throw new BasicBlankException("No card at row " + row + " and column " + column);
        }
        protected (int row, int column) GetRowColumnData(D thisCard)
        {
            int x;
            int y;
            int z = 0;
            var loopTo = Rows;
            for (x = 1; x <= loopTo; x++)
            {
                var loopTo1 = Columns;
                for (y = 1; y <= loopTo1; y++)
                {
                    var tempCard = ObjectList[z];
                    z += 1;
                    if (tempCard.Deck == thisCard.Deck)
                    {
                        return (x, y);// hopefully this works.
                    }
                }
            }
            throw new BasicBlankException("Can't find row/column data for Card With Deck Of " + thisCard.Deck);
        }
        public void TradeObject(int row, int column, D newObject)
        {
            var firstObject = GetObject(row, column);
            ObjectList.ReplaceItem(firstObject, newObject);
        }
        public void TradeObject(int oldDeck, D newObject)
        {
            var firstObject = ObjectList.GetSpecificItem(oldDeck);
            ObjectList.ReplaceItem(firstObject, newObject); //maybe no need to check for duplicates because its using dictionary now.
        }
        protected abstract Task ClickProcessAsync(D card); // this is where we need code for when clicking a card
        private IBasicEnableProcess? _networkProcess;
        private Func<bool>? _customFunction; // i think this will have no parameters for this one.
        public void SendEnableProcesses(IBasicEnableProcess nets, Func<bool> fun)
        {
            _networkProcess = nets;
            _customFunction = fun;
        }
        bool IControlObservable.CanExecute()
        {
            if (Visible == false)
            {
                return false;
            }
            if (IsEnabled == false)
            {
                return false;
            }
            return true;
        }
        public void ReportCanExecuteChange()
        {
            if (CommandContainer.IsExecuting == true && BusyCategory == EnumCommandBusyCategory.None)
            {
                IsEnabled = false; //i think
                return;
            }
            if (_networkProcess == null)
            {
                IsEnabled = true;
                return; //because you did not send something in.
            }
            if (_networkProcess.CanEnableBasics() == false)
            {
                IsEnabled = false;
                return;
            }
            IsEnabled = _customFunction!();
        }
        public EnumCommandBusyCategory BusyCategory { get; set; } = EnumCommandBusyCategory.None; //most of the time, none.
        public CommandContainer CommandContainer; //decided to make public so others can hook into it.
        public PlainCommand ObjectCommand { get; set; }

        private bool CanExecute(D card)
        {
            if (card.Visible == false)
            {
                return false;
            }
            if (IsEnabled == false)
            {
                return false;
            }
            return Visible; //for now, we have visible.  ma not 
        }
        public GameBoardObservable(CommandContainer container)
        {
            CommandContainer = container;
            MethodInfo method = this.GetPrivateMethod(nameof(ClickProcessAsync)); //hopefully protected is fine (?)
            MethodInfo fun = this.GetPrivateMethod(nameof(CanExecute));
            ObjectCommand = new PlainCommand(this, method, fun, null!, CommandContainer);
            CommandContainer.AddControl(this); //i think it should be here instead.
        }
    }
}