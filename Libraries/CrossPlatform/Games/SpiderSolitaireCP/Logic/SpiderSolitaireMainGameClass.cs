using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using SpiderSolitaireCP.Data;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace SpiderSolitaireCP.Logic
{
    [SingletonGame]
    public class SpiderSolitaireMainGameClass : SolitaireGameClass<SpiderSolitaireSaveInfo>
    {
        public SpiderSolitaireMainGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            )
            : base(solitaireData1, thisState, aggregator, score, command)
        {
            _score = score;
        }
        private WastePiles? _tempWaste;
        private readonly IScoreData _score;
        public override Task NewGameAsync()
        {
            _tempWaste = (WastePiles)_thisMod!.WastePiles1;
            return base.NewGameAsync();
        }
        protected async override Task ContinueOpenSavedAsync()
        {
            await base.ContinueOpenSavedAsync();
        }
        protected async override Task FinishSaveAsync()
        {
            await base.FinishSaveAsync();
        }
        protected override SolitaireCard CardPlayed()
        {
            var thisCard = base.CardPlayed();
            return thisCard;
        }
        protected override void AfterShuffleCards()
        {
            _thisMod!.MainPiles1!.ClearBoard();
            AfterShuffle();
        }
        public override void DrawCard()
        {
            CardList = _thisMod!.DeckPile!.DrawSeveralCards(10, false);
            _tempWaste!.AddCards(CardList);
            10.Times(x =>
            {
                if (_tempWaste.CanMoveAll(x - 1))
                {
                    MoveCards(x - 1);
                }
            });
        }
        private void MoveCards(int whichOne)
        {
            var tempList = _tempWaste!.MoveList();
            if (tempList.Count != 13)
            {
                throw new BasicBlankException("Must have 13 cards.  Otherwise, can't move the cards");
            }
            _thisMod!.MainPiles1!.AddCards(tempList);
            _tempWaste.RemoveCards(whichOne);
        }
        public override async Task AfterMovingCardsAsync(int whichOne)
        {
            if (_tempWaste!.CanMoveAll(whichOne) == false)
            {
                await base.AfterMovingCardsAsync(whichOne);
                return;
            }
            MoveCards(whichOne);
            if (_score.Score == HowManyCards)
            {
                await ShowWinAsync();
                return;
            }
            await base.AfterMovingCardsAsync(whichOne);
        }
    }
}