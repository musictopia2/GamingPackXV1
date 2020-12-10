using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.Exceptions;
using DemonSolitaireCP.Data;
using System.Linq;
namespace DemonSolitaireCP.Logic
{
    public class WastePiles : WastePilesCP
    {
        public WastePiles(CommandContainer command) : base(command)
        {
        }
        public override void ClearBoard(IDeckDict<SolitaireCard> thisCol)
        {
            base.ClearBoard(thisCol);
            Piles.PileList.ForEach(thisPile => thisPile.CardList.Add(thisCol[Piles.PileList.IndexOf(thisPile)]));
        }
        protected override void AfterRemovingFromLastPile(int Lasts)
        {
            if (Piles.HasCardInColumn(Lasts) || GlobalClass.MainModel!.Heel1.IsEndOfDeck())
            {
                return;
            }
            Piles.AddCardToColumn(Lasts, GlobalClass.MainModel.Heel1.DrawCard());
        }
        public override bool CanAddSingleCard(int whichOne, SolitaireCard thisCard)
        {
            if (Piles.HasCardInColumn(whichOne) == false)
            {
                if (GlobalClass.MainModel!.Heel1.IsEndOfDeck() == false)
                {
                    throw new BasicBlankException("If its not at the end of the deck; a card needs to be placed");
                }
                return true;
            }
            var prevCard = Piles.GetLastCard(whichOne);
            return prevCard.Value - 1 == thisCard.Value && prevCard.Suit != thisCard.Suit;
        }
        public override bool CanMoveCards(int whichOne, out int lastOne)
        {
            if (PreviousSelected == -1)
            {
                throw new BasicBlankException("Cannot find out whether we can move the cards because none was selected");
            }
            var firstList = Piles.ListGivenCards(PreviousSelected);
            TempList = firstList.ListValidCardsAlternateColors(); //has to use templist
            var thisPile = Piles.PileList[whichOne];
            lastOne = TempList.Count - 1;
            if (thisPile.CardList.Count == 0)
            {
                return true;
            }
            var prevCard = Piles.GetLastCard(whichOne);
            var thisCard = Piles.PileList[PreviousSelected].CardList.First();
            return prevCard.Value - 1 == thisCard.Value && prevCard.Suit != thisCard.Suit;
        }
        public override bool CanMoveToAnotherPile(int whichOne)
        {
            return false;
        }
    }
}