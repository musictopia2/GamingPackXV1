using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.DrawableListsObservable
{
    public class SingleObservablePile<D> : SimpleControlObservable where D : IDeckObject, new()
    {
        private readonly DeckRegularDict<D> _objectList;
        public event PileClickedEventHandler? PileClickedAsync;
        public delegate Task PileClickedEventHandler();
        public ControlCommand PileObjectClickedCommand { get; set; }
        public bool Visible { get; set; } = true;
        private async Task PrivatePileClickedAsync()
        {
            if (PileClickedAsync == null)
            {
                return;
            }
            await PileClickedAsync.Invoke();
        }
        public SingleObservablePile(CommandContainer command) : base(command) //realy don't want to have to resolve with this library.
        {
            _objectList = new DeckRegularDict<D>();
            MethodInfo method = this.GetPrivateMethod(nameof(PrivatePileClickedAsync));
            PileObjectClickedCommand = new ControlCommand(this, method, command);
        }
        public D CurrentCard { get; private set; } = new D();
        private int _previousNum; // used to doublecheck
        private bool _isFirst;
        public string Text { get; set; } = "Discard";
        private void NotifyCommands()
        {
            PileObjectClickedCommand.ReportCanExecuteChange();
        }
        public SavedDiscardPile<D> GetSavedPile()
        {
            SavedDiscardPile<D> output = new SavedDiscardPile<D>();
            output.CurrentCard = CurrentCard;
            output.PileList = _objectList.ToRegularDeckDict();
            return output;
        }
        public void SavedDiscardPiles(SavedDiscardPile<D> save)
        {
            _objectList.Clear();
            if (save.PileList.Count == 0 && save.CurrentCard.Deck == 0)
            {
                CurrentCard = new D();
                return;
            }
            CurrentCard = save.CurrentCard;
            _objectList.ReplaceRange(save.PileList);
            _previousNum = _objectList.Count;
        }
        public void MakeKnown()
        {
            CurrentCard.IsUnknown = false;
        }
        public bool CanCutDiscard()
        {
            if (_objectList.Count < 4)
            {
                return false;
            }
            return true;
        }
        public void NewList(IDeckDict<D> whatList)
        {
            if (whatList.Count == 0)
            {
                return;
            }
            CurrentCard = whatList.First();
            CurrentCard.IsUnknown = false;
            CurrentCard.IsSelected = false;
            CurrentCard.Drew = false;
            whatList.RemoveFirstItem(); //try this way.  could fix at least one of the problems.
            whatList.ForEach(Items =>
            {
                Items.IsSelected = false;
                Items.Drew = false;
            });
            _objectList.ReplaceRange(whatList); // because we are replacing the entire range.
            _previousNum = _objectList.Count;
        }
        public bool CardExist(int deck)
        {
            if (deck == CurrentCard.Deck)
            {
                return true;
            }
            return _objectList.ObjectExist(deck);
        }
        public void AddSeveralCards(IDeckDict<D> whatList)
        {
            if (whatList.Count == 0)
            {
                return;
            }
            DeckRegularDict<D> newList = new DeckRegularDict<D>();
            if (CurrentCard.Deck > 0)
            {
                newList.Add(CurrentCard);
            }
            CurrentCard = whatList.Last();
            CurrentCard.IsUnknown = false;
            CurrentCard.IsSelected = false;
            CurrentCard.Drew = false;
            int x;
            for (x = whatList.Count - 1; x >= 1; x += -1)
            {
                whatList[x - 1].IsSelected = false;
                whatList[x - 1].Drew = false; // i think
                newList.Add(whatList[x - 1]);
            }
            _objectList.AddRange(newList);
            _previousNum = _objectList.Count; // i think
        }
        public int CardsLeft()
        {
            if (CurrentCard.Deck == 0)
            {
                return 0;
            }
            return _objectList.Count + 1;
        }
        public DeckRegularDict<D> FlipCardList() // will need the actual CardList.CardList
        {
            DeckRegularDict<D> tempList = _objectList.ToRegularDeckDict();
            tempList.Reverse();
            tempList.Add(CurrentCard); //i think
            ClearCards();
            return tempList;
        }
        public int HowManyInDiscard()
        {
            return _objectList.Count;
        }
        public DeckRegularDict<D> DiscardList()
        {
            return DiscardList(_objectList.Count);
        }
        public DeckRegularDict<D> DiscardList(int howManyToKeep)
        {
            if (_objectList.Count != _previousNum)
            {
                throw new BasicBlankException("The list is wrong"); // 
            }
            return _objectList.Take(howManyToKeep).ToRegularDeckDict(); //try this way
        }

        public D GetCardInfo()
        {
            return GetCardInfo(CurrentCard.Deck);
        }
        public D GetCardInfo(int deck)
        {
            if (deck == 0)
            {
                throw new BasicBlankException("The deck cannot be 0 when trying to getcardinfo.  Use PileEmpty function to determine whether the card even exists or not");
            }
            if (CurrentCard.Deck == deck)
            {
                return CurrentCard;
            }
            return _objectList.GetSpecificItem(deck);
        }
        public int CardSelected()
        {
            if (CurrentCard.Deck == 0)
            {
                return 0;
            }
            if (CurrentCard.IsSelected == false)
            {
                return 0;
            }
            return CurrentCard.Deck;
        }
        public void HighlightCard()
        {
            CurrentCard.Drew = true;
        }
        public void ClearCards()
        {
            _objectList.Clear();
            _previousNum = 0;
            if (CurrentCard.Deck == 0)
            {
                return;
            }
            CurrentCard = new D();
        }
        public void UnhighlightCard()
        {
            CurrentCard.Drew = false;
        }
        public void UnselectCard()
        {
            CurrentCard.IsSelected = false;
        }
        public void IsSelected(bool selects)
        {
            CurrentCard.IsSelected = selects;
        }
        public void CardsReshuffled(int howManyToKeep)
        {
            DeckRegularDict<D> thisCol = _objectList.Skip(_objectList.Count - howManyToKeep + 1).ToRegularDeckDict();
            _objectList.ReplaceRange(thisCol); //try this way.
            CurrentCard.IsSelected = false;
            CurrentCard.Drew = false;
            _previousNum = _objectList.Count;
        }
        public void CardsReshuffled()
        {
            _objectList.Clear();
            CurrentCard.IsSelected = false;
            CurrentCard.Drew = false;
            _previousNum = 0;
        }
        public void CutDeck()
        {
            if (_objectList.Count < 4)
            {
                throw new BasicBlankException("Cannot cut deck because there are less then 4 cards left");
            }
            int index;
            index = _objectList.Count / 2;
            if (_objectList.ObjectExist(CurrentCard.Deck))
            {
                _objectList.RemoveObjectByDeck(CurrentCard.Deck); //try this way.  not sure if it will later cause problems but its a risk to try to take.
            }
            _objectList.InsertMiddle(index, CurrentCard);
            RemoveFromPile();
        }
        public void AddRestOfDeck(IDeckDict<D> currentList)
        {
            if (currentList.ObjectExist(_objectList.First().Deck))
            {
                _objectList.RemoveFirstItem(); //try to remove the first item.  hopefully does not cause another issue.
            }
            _objectList.AddRange(currentList);
            _previousNum = _objectList.Count; // so far should be fine
        }
        public void AddCard(D thisCard)
        {
            if (thisCard.Deck == CurrentCard.Deck)
            {
                return;
            }
            if (thisCard.Deck == -1)
            {
                throw new Exception("Cannot send a blank card.  Find out what happened");
            }
            thisCard.IsUnknown = false;
            thisCard.IsSelected = false;
            thisCard.Drew = false; // i think
            if (CurrentCard.Deck == 0)
            {
                _previousNum = 0;
                _objectList.Clear();
            }
            else
            {
                if (CurrentOnly == false)
                {
                    if (_objectList.ObjectExist(thisCard.Deck) == true)
                    {
                        _objectList.RemoveObjectByDeck(thisCard.Deck);
                    }
                    if (_objectList.ObjectExist(CurrentCard.Deck))
                    {
                        _objectList.RemoveObjectByDeck(CurrentCard.Deck); //try this way.  not sure what problem this will cause.
                    }
                    _objectList.Add(CurrentCard);
                    _previousNum = _objectList.Count;
                }
                else
                {
                    CurrentCard = thisCard; // try this 
                    return; // try this way.
                }

            }
            FirstLoad(thisCard, thisCard.Deck);
        }
        public void FirstLoad(D thisCard)
        {
            FirstLoad(thisCard, 0);
        }
        private void FirstLoad(D thisCard, int deck)
        {
            if (_isFirst == true)
            {
                return;
            }
            if (deck > 0)
            {
                CurrentCard = thisCard;
            }
            _isFirst = false;
        }
        public void RemoveCardFromPile(D thisCard)
        {
            if (thisCard.Deck == CurrentCard.Deck)
            {
                RemoveFromPile();
                return;
            }
            _objectList.RemoveSpecificItem(thisCard);
            _previousNum = _objectList.Count; // this way if a card has to get played again like fluxx; won't cause the problem that happened before
        }
        public void RemoveFromPile()
        {
            if (CurrentCard.Deck == 0)
            {
                throw new BasicBlankException("Cannot remove from discard because there are no cards to remove");
            }
            if (_objectList.Count == 0)
            {
                _previousNum = 0;
                CurrentCard = new D();
                return;
            }
            CurrentCard = _objectList.Last();
            _objectList.RemoveLastItem();
            _previousNum = _objectList.Count;
        }
        public bool PileEmpty()
        {
            if (CurrentCard.Deck == 0)
            {
                return true;
            }
            return false;
        }

        protected override void EnableChange()
        {
            NotifyCommands();
        }
        protected override void PrivateEnableAlways() { }
        public bool CurrentOnly { get; set; }
    }
}