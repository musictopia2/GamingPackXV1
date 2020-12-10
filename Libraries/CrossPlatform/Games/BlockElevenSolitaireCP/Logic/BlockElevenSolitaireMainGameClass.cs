using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using BlockElevenSolitaireCP.Data;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BlockElevenSolitaireCP.Logic
{
    [SingletonGame]
    public class BlockElevenSolitaireMainGameClass : SolitaireGameClass<BlockElevenSolitaireSaveInfo>
    {
        private readonly IScoreData _score;
        public BlockElevenSolitaireMainGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            )
            : base(solitaireData1, thisState, aggregator, score, command)
        {
            _score = score;
        }
        public override Task NewGameAsync()
        {
            _thisMod!.MainPiles1!.SetSavedScore(0);
            _thisMod.DeckPile!.ClearCards(); //just in case.
            return base.NewGameAsync();
        }
        protected override bool HasWon(int scores)
        {
            return scores == 40;
        }
        protected override void AfterMoveSingleCard()
        {
            _score.Score += 2; //try this way.
        }
        protected override SolitaireCard CardPlayed()
        {
            var thisCard = base.CardPlayed();
            return thisCard;
        }
        protected override void AfterShuffleCards()
        {
            var thisList = CardList.Where(items => items.Value <= EnumRegularCardValueList.Ten).Take(12).ToRegularDeckDict();
            CardList!.RemoveGivenList(thisList);
            var thisCard = CardList.Where(items => items.Value >= EnumRegularCardValueList.Jack).Take(1).Single();
            CardList.RemoveSpecificItem(thisCard);
            thisList.ForEach(tempCard => CardList.InsertBeginning(tempCard));
            CardList.Add(thisCard); //put to last.
            AfterShuffle();
        }
        protected async override Task ContinueOpenSavedAsync()
        {
            await base.ContinueOpenSavedAsync();
        }
        protected async override Task FinishSaveAsync()
        {
            await base.FinishSaveAsync();
        }
    }
}