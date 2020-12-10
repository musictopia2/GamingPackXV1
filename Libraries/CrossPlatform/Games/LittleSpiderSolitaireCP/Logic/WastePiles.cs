using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
namespace LittleSpiderSolitaireCP.Logic
{
    public class WastePiles : WastePilesCP
    {
        public WastePiles(CommandContainer command) : base(command)
        {
        }
        public void AddCards(DeckRegularDict<SolitaireCard> thisList)
        {
            if (thisList.Count != 8)
            {
                throw new BasicBlankException($"Needs 8 cards, not {thisList.Count}");
            }
            int x = 0;
            Discards!.PileList!.ForEach(thisPile =>
            {
                thisPile.ObjectList.Add(thisList[x]); //has to be 0 based.
                x++;
            });
        }
        protected override void AfterFirstLoad()
        {
            var thisList = Enumerable.Range(4, 4).ToCustomBasicList();
            Discards!.RemoveSpecificDiscardPiles(thisList);
        }
        public override void ClearBoard(IDeckDict<SolitaireCard> thisCol)
        {
            base.ClearBoard(thisCol);
            int x = 0;
            Discards!.PileList!.ForEach(thisPile =>
            {
                thisPile.ObjectList.Add(thisCol[x]); //has to be 0 based.
                x++;
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
            if (Discards!.HasCard(whichOne) == false)
            {
                return false; //because not filled.
            }
            var oldCard = Discards.GetLastCard(whichOne);
            var newCard = Discards.GetLastCard(PreviousSelected);
            if (oldCard.Value - 1 == newCard.Value)
            {
                return true;
            }
            if (oldCard.Value + 1 == newCard.Value)
            {
                return true;
            }
            if (oldCard.Value == EnumRegularCardValueList.King && newCard.Value == EnumRegularCardValueList.LowAce)
            {
                return true;
            }
            return oldCard.Value == EnumRegularCardValueList.LowAce && newCard.Value == EnumRegularCardValueList.King;
        }
    }
}