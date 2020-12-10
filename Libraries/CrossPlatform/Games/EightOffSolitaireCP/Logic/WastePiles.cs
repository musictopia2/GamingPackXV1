using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace EightOffSolitaireCP.Logic
{
    public class WastePiles : WastePilesCP
    {
        public WastePiles(CommandContainer command) : base(command)
        {
        }
        public override void ClearBoard(IDeckDict<SolitaireCard> thisCol)
        {
            base.ClearBoard(thisCol);
            int y = 0;
            6.Times(x =>
            {
                Piles.PileList.ForEach(thisPile =>
                {
                    thisPile.CardList.Add(thisCol[y]);
                    y++;
                });
            });
        }
        public override bool CanAddSingleCard(int whichOne, SolitaireCard thisCard)
        {
            if (Piles.HasCardInColumn(whichOne) == false)
            {
                return thisCard.Value == EnumRegularCardValueList.King;
            }
            var oldCard = Piles.GetLastCard(whichOne);
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
            return false;
        }
    }
}