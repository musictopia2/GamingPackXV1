using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.StockClasses
{
    public class DiscardPilesVM<D> : BasicMultiplePilesCP<D>
        where D : IDeckObject, new()
    {
        private readonly CommandContainer _command;
        public void AddToEmptyDiscard(D thisCard)
        {
            foreach (var thisPile in PileList!)
                if (thisPile.ObjectList.Count == 0)
                {
                    thisPile.ObjectList.Add(thisCard);
                    return;
                }
            throw new BasicBlankException("There was no empty piles left.  Find out what happened");
        }
        public void AddToDiscard(int pile, D thisCard)
        {
            PileList![pile - 1].ObjectList.Add(thisCard);
        }
        public void ClearCards() // hopefully this simple (?)
        {
            foreach (var pile in PileList!)
            {
                pile.ObjectList.Clear();// you need to clear all the cards in each pile.
            }
        }
        public int CardSelected(out int whichPile) //try out.  if we somehow need to change to ref, we can.
        {
            int x = 0;
            D thisCard;
            foreach (var thisPile in PileList!)
            {
                if (thisPile.ObjectList.Count > 0)
                {
                    thisCard = thisPile.ObjectList.Last();
                    if (thisCard.IsSelected == true)
                    {
                        whichPile = x;
                        return thisCard.Deck;
                    }
                }
                x += 1;
            }
            whichPile = -1;
            return -1;
        }
        public bool CanAddToDiscardFlinch(int pile) // needs to be 0 based for now on.
        {
            BasicPileInfo<D> thisPile;
            thisPile = PileList![pile];
            if (thisPile.ObjectList.Count == 0)
            {
                return true;
            }
            if (HasEmptyPile() == true)
            {
                return false;
            }
            return true;
        }
        private bool HasEmptyPile()
        {
            foreach (var thisPile in PileList!)
            {
                if (thisPile.ObjectList.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void RemoveCard(int pile, int deck)
        {
            BasicPileInfo<D> thisPile;
            thisPile = PileList![pile];
            thisPile.ObjectList.RemoveObjectByDeck(deck);
            thisPile.IsSelected = false; //just in case.
        }
        public void Init(int howManyPiles)
        {
            Columns = howManyPiles;
            Rows = 1;
            Style = EnumMultiplePilesStyleList.HasList;
            HasFrame = true;
            HasText = true;
            LoadBoard();
            foreach (var thisPile in PileList!)
            {
                thisPile.Text = $"Discard {PileList.IndexOf(thisPile) + 1}";
            }
        }
        public void CopyDiscards(out DiscardPilesVM<D> newDiscard)
        {
            newDiscard = new DiscardPilesVM<D>(_command);
            newDiscard.Init(Columns);
            int x = 0;
            foreach (var thisPile in PileList!)
            {
                x += 1;
                var newPile = newDiscard.PileList![x - 1]; // because 0 based
                foreach (var thisCard in thisPile.ObjectList)
                {
                    newPile.ObjectList.Add(thisCard);// i think this is enough
                }
            }
        }
        public int NextToLastDeck(int pile)
        {
            var thisPile = PileList![pile]; // needs to be 0 based
            D thisCard;
            thisCard = thisPile.ObjectList[thisPile.ObjectList.Count - 1];
            return thisCard.Deck;
        }
        public int FirstDeck(int pile) //i sent in 1 based  if i am wrong, fix.
        {
            D thisCard;
            var thisPile = PileList![pile - 1];
            thisCard = thisPile.ObjectList.First();
            return thisCard.Deck;
        }
        public void UnselectAllCards()
        {
            int x = 0;
            foreach (var thisPile in PileList!)
            {
                x += 1;
                UnselectPile(x - 1); // because 0 based
            }
        }
        public DiscardPilesVM(CommandContainer command) : base(command)
        {
            _command = command;
        }
    }
}