using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace GrandfathersClockCP.Logic
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
            5.Times(x =>
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
            return false;
        }

        public override bool CanMoveCards(int whichOne, out int lastOne)
        {
            lastOne = -1; //until i figure out what else to do.
            return false;
        }

        public override bool CanMoveToAnotherPile(int whichOne)
        {
            if (Piles.HasCardInColumn(whichOne) == false)
            {
                return true;
            }
            var oldCard = Piles.GetLastCard(whichOne);
            var newCard = GetCard();
            if (oldCard.Value == EnumRegularCardValueList.LowAce && newCard.Value == EnumRegularCardValueList.King)
            {
                return true; // because round corner.
            }
            return oldCard.Value - 1 == newCard.Value;
        }
    }
}