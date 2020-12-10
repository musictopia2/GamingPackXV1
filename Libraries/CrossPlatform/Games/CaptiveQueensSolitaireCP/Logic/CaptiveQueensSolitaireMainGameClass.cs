using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CaptiveQueensSolitaireCP.Data;
using CommonBasicStandardLibraries.Messenging;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace CaptiveQueensSolitaireCP.Logic
{
    [SingletonGame]
    public class CaptiveQueensSolitaireMainGameClass : SolitaireGameClass<CaptiveQueensSolitaireSaveInfo>
    {
        public CaptiveQueensSolitaireMainGameClass(ISolitaireData solitaireData1,
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
            DeckRegularDict<SolitaireCard> output = new DeckRegularDict<SolitaireCard>
            {
                FindCardBySuitValue(EnumRegularCardValueList.Queen, EnumSuitList.Spades),
                FindCardBySuitValue(EnumRegularCardValueList.Queen, EnumSuitList.Diamonds),
                FindCardBySuitValue(EnumRegularCardValueList.Queen, EnumSuitList.Clubs),
                FindCardBySuitValue(EnumRegularCardValueList.Queen, EnumSuitList.Hearts)
            };
            CardList!.RemoveGivenList(output);
            output.Reverse();
            output.ForEach(thisCard => CardList.InsertBeginning(thisCard));
            _thisMod!.MainPiles1!.ClearBoard();
            AfterShuffle();
        }
    }
}