using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using BlockElevenSolitaireCP.Data;
using CommonBasicStandardLibraries.Exceptions;
namespace BlockElevenSolitaireCP.Logic
{
    public class WastePiles : WastePilesCP
    {
        public WastePiles(CommandContainer command) : base(command)
        {
        }
        public override void ClearBoard(IDeckDict<SolitaireCard> thisCol)
        {
            base.ClearBoard(thisCol);
            DealOutCards(thisCol);
        }
        public override bool CanAddSingleCard(int whichOne, SolitaireCard thisCard)
        {
            return false;
        }
        public override void RemoveSingleCard()
        {

        }
        public override void MoveSingleCard(int whichOne)
        {
            if (GlobalClass.MainMod!.DeckPile!.IsEndOfDeck() == true)
            {
                throw new BasicBlankException("Can't be at the end of the deck.  If so; alot of rethinking is required");
            }
            var thisCard = GlobalClass.MainMod.DeckPile.DrawCard();
            Discards!.RemoveCardFromPile(PreviousSelected);
            Discards.RemoveCardFromPile(whichOne);
            Discards.PileList!.ForEach(thisPile => thisPile.IsSelected = false); //try this way.
            Discards.AddCardToPile(PreviousSelected, thisCard);
            if (GlobalClass.MainMod.DeckPile.IsEndOfDeck())
            {
                throw new BasicBlankException("Can't be at the end of the deck for the second card.");
            }
            thisCard = GlobalClass.MainMod.DeckPile.DrawCard();
            Discards.AddCardToPile(whichOne, thisCard);
            PreviousSelected = -1;
        }
        public override bool CanMoveCards(int whichOne, out int lastOne)
        {
            lastOne = -1; //until i figure out what else to do.
            return false;
        }
        public override bool CanMoveToAnotherPile(int whichOne)
        {
            if (Discards!.HasCard(whichOne) == false)
            {
                throw new BasicBlankException($"Must have at least one card on {whichOne}");
            }
            var oldCard = Discards.GetLastCard(whichOne);
            var newCard = Discards.GetLastCard(PreviousSelected);
            return (int)oldCard.Value + newCard.Value == EnumRegularCardValueList.Jack;
        }
    }
}