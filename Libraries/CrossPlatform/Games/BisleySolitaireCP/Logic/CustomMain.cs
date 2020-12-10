using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
namespace BisleySolitaireCP.Logic
{
    public class CustomMain : MainPilesCP
    {
        private readonly IScoreData _thisMod;
        public CustomMain(IScoreData thisMod, CommandContainer command, IGamePackageResolver resolver) : base(thisMod, command, resolver)
        {
            _thisMod = thisMod;
        }
        public override bool CanAddCard(int pile, SolitaireCard thisCard)
        {
            if (pile > 3)
            {
                return base.CanAddCard(pile, thisCard);
            }
            //for this; starts from kings and moves down
            if (Piles.HasCard(pile) == false)
            {
                return thisCard.Value == EnumRegularCardValueList.King;
            }
            var oldCard = Piles.GetLastCard(pile);
            if (oldCard.Suit != thisCard.Suit)
            {
                return false;
            }
            return oldCard.Value == thisCard.Value + 1;
        }
        public override void ClearBoard(IDeckDict<SolitaireCard> thisList)
        {
            if (thisList.Count != CardsNeededToBegin)
            {
                throw new BasicBlankException($"Needs {CardsNeededToBegin} not {thisList.Count}");
            }
            if (CardsNeededToBegin != 4)
            {
                throw new BasicBlankException("Cards To begin with must be 4");
            }
            _thisMod.Score = thisList.Count;
            Piles.ClearBoard();
            var tempList = Piles.PileList.Skip(4).ToCustomBasicList();
            if (tempList.Count == 0)
            {
                throw new BasicBlankException("There has to be 4 more piles left.  Rethink");
            }
            tempList.ForEach(thisPile =>
            {
                thisPile.ObjectList.Add(thisList[tempList.IndexOf(thisPile)]);
            });
        }
    }
}