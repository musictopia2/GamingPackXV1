using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using EagleWingsSolitaireCP.Data;
namespace EagleWingsSolitaireCP.Logic
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
        public override bool CanAddSingleCard(int whichOne, SolitaireCard thisCard)
        {
            if (Piles.HasCardInColumn(whichOne) == false && GlobalClass.MainModel!.Heel1.IsEndOfDeck())
            {
                return true;
            }
            return Piles.HasCardInColumn(whichOne) == false && GlobalClass.MainModel!.Heel1!.CardsLeft() == 1;
        }
        public override bool CanMoveCards(int whichOne, out int lastOne)
        {
            lastOne = -1;
            return false;
        }
        public override bool CanMoveToAnotherPile(int whichOne)
        {
            return false;
        }
        protected override void AfterRemovingFromLastPile(int Lasts)
        {
            if (Piles.HasCardInColumn(Lasts) || GlobalClass.MainModel!.Heel1!.IsEndOfDeck() || GlobalClass.MainModel.Heel1.CardsLeft() == 1)
            {
                return;
            }
            Piles.AddCardToColumn(Lasts, GlobalClass.MainModel.Heel1.DrawCard());
            if (GlobalClass.MainModel.Heel1.CardsLeft() == 1)
            {
                GlobalClass.MainModel.Heel1.DeckStyle = EnumDeckPileStyle.AlwaysKnown;
            }
        }
    }
}