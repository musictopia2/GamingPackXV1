using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.Exceptions;
namespace EasyGoSolitaireCP.Logic
{
    public class WastePiles : WastePilesCP
    {
        public WastePiles(CommandContainer command) : base(command)
        {
        }
        public override void ClearBoard(IDeckDict<SolitaireCard> thisCol)
        {
            base.ClearBoard(thisCol);
            int x = 0;
            Discards!.PileList!.ForEach(thisPile =>
            {
                thisPile.ObjectList.Add(thisCol[x]);
                x++;
            });
        }
        public override bool CanAddSingleCard(int whichOne, SolitaireCard thisCard)
        {
            if (Discards!.HasCard(whichOne) == false)
            {
                return true;
            }
            var oldCard = Discards.GetLastCard(whichOne);
            if (thisCard.Suit != oldCard.Suit)
            {
                return false;
            }
            return oldCard.Value - 1 == thisCard.Value;
        }

        public override bool CanMoveCards(int whichOne, out int lastOne)
        {
            lastOne = -1; //until i figure out what else to do.
            return false;
        }

        public override bool CanMoveToAnotherPile(int whichOne)
        {
            if (PreviousSelected == -1)
            {
                throw new BasicBlankException("There is no pile even selected");
            }
            if (Discards!.HasCard(whichOne) == false)
            {
                return false; //can only fill an empty spot with a card from the deck
            }
            var oldCard = Discards.GetLastCard(whichOne);
            var newCard = Discards.GetLastCard(PreviousSelected);
            if (newCard.Suit != oldCard.Suit)
            {
                return false;
            }
            return oldCard.Value - 1 == newCard.Value;
        }
    }
}