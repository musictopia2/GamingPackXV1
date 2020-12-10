using BakersDozenSolitaireCP.Data;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BakersDozenSolitaireCP.Logic
{
    [SingletonGame]
    public class BakersDozenSolitaireMainGameClass : SolitaireGameClass<BakersDozenSolitaireSaveInfo>
    {
        public BakersDozenSolitaireMainGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            )
            : base(solitaireData1, thisState, aggregator, score, command)
        {
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
            var firstList = DeckList.Where(items => items.Value == EnumRegularCardValueList.King).ToRegularDeckDict();
            DeckList.ReshuffleFirstObjects(firstList, 0, 12);
            CardList = DeckList.ToRegularDeckDict();
            _thisMod!.MainPiles1!.ClearBoard();
            AfterShuffle();
        }
    }
}