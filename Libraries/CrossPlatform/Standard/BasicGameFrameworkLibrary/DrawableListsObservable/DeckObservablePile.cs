using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.DrawableListsObservable
{
    public class DeckObservablePile<D> : SimpleControlObservable where D : IDeckObject, new()//decided to keep generics for this.
    {
        private readonly DeckRegularDict<D> _objectList;
        public event Func<Task>? DeckClickedAsync;
        private void CommandChanges()
        {
            DeckObjectClickedCommand.ReportCanExecuteChange();
        }
        public ControlCommand DeckObjectClickedCommand { get; set; }
        private async Task PrivateDeckClickedAsync()
        {
            if (DeckClickedAsync == null)
            {
                return;
            }
            await DeckClickedAsync.Invoke();
        }
        public DeckObservablePile(CommandContainer command) : base(command) //realy don't want to have to resolve with this library.
        {
            _objectList = new DeckRegularDict<D>();
            MethodInfo method = this.GetPrivateMethod(nameof(PrivateDeckClickedAsync));
            DeckObjectClickedCommand = new ControlCommand(this, method, command);
        }

        private EnumDeckPileStyle _deckStyle = EnumDeckPileStyle.Unknown;

        public EnumDeckPileStyle DeckStyle
        {
            get { return _deckStyle; }
            set
            {
                if (SetProperty(ref _deckStyle, value))
                {
                    _objectList.ForEach(x =>
                    {
                        if (value == EnumDeckPileStyle.AlwaysKnown)
                        {
                            x.IsUnknown = false;
                        }
                        else
                        {
                            x.IsUnknown = true;
                        }
                    });
                }
            }
        }
        public string TextToAppear { get; private set; } = "";
        public D CurrentCard { get; private set; } = new D();

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (SetProperty(ref _isSelected, value) == true)
                {
                    CurrentCard.IsSelected = value;
                }
            }
        }
        public bool NeverAutoDisable { get; set; }
        public bool CanCutDeck() //used for hit the deck
        {
            return _objectList.Count >= 5;
        }
        public bool CanFlipDeck()
        {
            return _objectList.Count > 1;
        }
        public bool IsEndOfDeck()
        {
            return _objectList.Count == 0;
        }
        public int CardsLeft()
        {
            return _objectList.Count;
        }
        public void ClearCards()
        {
            _objectList.Clear();
            UpdateCards();
        }
        public void ShuffleInExtraCards(IDeckDict<D> extraCards) //this is needed for games like life card game where some cards can't be in hand but must be in deck.
        {
            foreach (var thisCard in extraCards)
                if (DeckStyle == EnumDeckPileStyle.AlwaysKnown)
                {
                    thisCard.IsUnknown = false;
                }
                else
                {
                    thisCard.IsUnknown = true;
                }
            _objectList.AddRange(extraCards);
            _objectList.ShuffleList();//i think we need this too.
            UpdateCards();
        }
        public void InsertBeginningCards(IDeckDict<D> testCards) //this is needed in cases where we need some cards to be at the beginning of the deck.  one hint was tee it up.
        {
            testCards.ForEach(tempCard =>
            {
                var ourCard = _objectList.GetSpecificItem(tempCard.Deck);
                _objectList.RemoveSpecificItem(ourCard);
                _objectList.InsertBeginning(tempCard);
            });
            UpdateCards();
        }
        public DeckRegularDict<D> DeckList() // try this way
        {
            _objectList.ForEach(thisO => thisO.Visible = true);
            return _objectList.ToRegularDeckDict();
        }
        public DeckRegularDict<D> SavedList()
        {
            return _objectList.ToRegularDeckDict();
        }
        public DeckRegularDict<D> DrawSeveralCards(int howMany, bool showErrors = true)
        {
            if (_objectList.Count < howMany && showErrors)
            {
                throw new BasicBlankException("Not enough cards to draw.  Needs at least " + howMany + ".  There are " + _objectList.Count + " cards left");
            }
            else if (_objectList.Count < howMany)
            {
                howMany = _objectList.Count;
            }
            DeckRegularDict<D> tempList = _objectList.Take(howMany).ToRegularDeckDict();
            _objectList.RemoveGivenList(tempList, NotifyCollectionChangedAction.Remove); //try this way.  so the event happens once, and not several times.
            tempList.MakeAllObjectsKnown();
            UpdateCards();
            return tempList;
        }
        private void UpdateCards()
        {
            if (_objectList.Count == 0)
            {
                CurrentCard = new D();
            }
            else
            {
                CurrentCard = _objectList.First();
            }
            UpdateText();
        }
        public D DrawCard()
        {
            if (_objectList.Count == 0)
            {
                throw new BasicBlankException("Cannot draw a card because there are no more cards left");
            }
            D thisCard;
            thisCard = _objectList.First();
            _objectList.RemoveFirstItem();
            thisCard.IsUnknown = false;
            UpdateCards();
            return thisCard;
        }

        /// <summary>
        /// this is used in cases where you have to remove a specific item but return the item.  for games like hit the deck and even uno.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public D DrawCard(int deck)
        {
            D output = _objectList.GetSpecificItem(deck);
            _objectList.RemoveSpecificItem(output);
            output.IsUnknown = false;
            UpdateCards();
            return output;
        }
        public void ManuallyRemoveSpecificCard(D thisCard)
        {
            thisCard.IsUnknown = false; // is not unknown because its being drawn manually (like in games like cousin)
            _objectList.RemoveSpecificItem(thisCard);
            UpdateCards();
        }
        public void AddCard(D thisCard)
        {
            _objectList.Add(thisCard);
            UpdateCards();
        }
        public void PutInMiddle(D thisCard)
        {
            int index;
            index = _objectList.Count / 2;
            if (DeckStyle == EnumDeckPileStyle.Unknown)
            {
                thisCard.IsUnknown = true; //i think if putting in middle, should be unknown
            }
            else
            {
                thisCard.IsUnknown = false;
            }
            _objectList.InsertMiddle(index, thisCard);
            UpdateCards();
        }
        public D CutTheDeck()
        {
            D thisCard;
            int index;
            index = _objectList.Count / 2;
            thisCard = _objectList[index];
            _objectList.RemoveSpecificItem(thisCard);
            UpdateCards();
            return thisCard;
        }
        public CustomBasicList<int> GetCardIntegers()
        {
            return (from Items in _objectList
                    select Items.Deck).ToCustomBasicList();
        }
        public DeckRegularDict<D> FlipCardList()
        {
            _objectList.Reverse();
            DeckRegularDict<D> TempList = _objectList.ToRegularDeckDict();
            ClearCards();
            return TempList;
        }
        public D RevealCard()
        {
            if (_objectList.Count == 0)
            {
                throw new BasicBlankException("Cannot reveal a card because there are no more cards left");
            }
            return _objectList.First();
        }
        public void HideText()
        {
            _newText = ""; //this is used in cases where not enough room.  in that case, can just hide it.
        }
        private string _newText = "Deck";
        public void InsertText(string whatText)
        {
            _newText = whatText;
            UpdateText();
        }
        public bool CardExists(int deck)
        {
            return _objectList.ObjectExist(deck); // i think
        }

        private void UpdateText()
        {
            if (DrawInCenter == false)
            {
                TextToAppear = $"{_newText} ({_objectList.Count})";
            }
            else
            {
                TextToAppear = _objectList.Count.ToString();
            }
        }

        public bool DrawInCenter { get; set; }
        public void OriginalList(IEnumerableDeck<D> thisList)
        {
            _objectList.ReplaceRange(thisList); // i think
            foreach (var thisCard in thisList)
            {
                if (DeckStyle == EnumDeckPileStyle.AlwaysKnown)
                {
                    thisCard.IsUnknown = false;
                }
                else
                {
                    thisCard.IsUnknown = true;
                }
            }
            UpdateCards();
        }
        protected override bool CanEnableFirst()
        {
            if (_objectList.Count == 0 && NeverAutoDisable == false)
            {
                return false;
            }
            return base.CanEnableFirst();
        }
        protected override void EnableChange()
        {
            CommandChanges();
        }
        protected override void PrivateEnableAlways() { }

    }
}