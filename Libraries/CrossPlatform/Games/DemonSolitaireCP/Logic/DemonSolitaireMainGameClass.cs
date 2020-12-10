using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using DemonSolitaireCP.Data;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace DemonSolitaireCP.Logic
{
    [SingletonGame]
    public class DemonSolitaireMainGameClass : SolitaireGameClass<DemonSolitaireSaveInfo>
    {
        public DemonSolitaireMainGameClass(ISolitaireData solitaireData1,
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
            var newList = SaveRoot.HeelList.GetNewObjectListFromDeckList(DeckList);
            GlobalClass.MainModel!.Heel1.OriginalList(newList);
            await base.ContinueOpenSavedAsync();
        }
        protected async override Task FinishSaveAsync()
        {
            SaveRoot.HeelList = GlobalClass.MainModel!.Heel1.GetCardIntegers();
            await base.FinishSaveAsync();
        }
        protected override SolitaireCard CardPlayed()
        {
            var thisCard = base.CardPlayed();
            return thisCard;
        }

        protected override void AfterShuffleCards()
        {
            var thisCol = CardList.Take(13).ToRegularDeckDict();
            CardList!.RemoveRange(0, 13);
            GlobalClass.MainModel!.Heel1.OriginalList(thisCol);
            thisCol = CardList.Take(1).ToRegularDeckDict();
            CardList.RemoveRange(0, 1);
            AfterShuffle(thisCol);
            CardList.Clear();
        }
    }
}