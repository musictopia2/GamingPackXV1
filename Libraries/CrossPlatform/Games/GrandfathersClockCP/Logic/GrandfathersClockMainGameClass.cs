using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using GrandfathersClockCP.Data;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace GrandfathersClockCP.Logic
{
    [SingletonGame]
    public class GrandfathersClockMainGameClass : SolitaireGameClass<GrandfathersClockSaveInfo>
    {
        public GrandfathersClockMainGameClass(ISolitaireData solitaireData1,
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
            //anything else you need will be here
            await base.ContinueOpenSavedAsync();
        }
        protected async override Task FinishSaveAsync()
        {
            //anything else to finish save will be here.
            await base.FinishSaveAsync();
        }
        protected override SolitaireCard CardPlayed()
        {
            var thisCard = base.CardPlayed();
            return thisCard;
            //if any changes, will be here.
        }
        protected override void AfterShuffleCards()
        {
            DeckRegularDict<SolitaireCard> thisList = new DeckRegularDict<SolitaireCard>
            {
                FindCardBySuitValue(EnumRegularCardValueList.Ten, EnumSuitList.Hearts),
                FindCardBySuitValue(EnumRegularCardValueList.Jack, EnumSuitList.Spades),
                FindCardBySuitValue(EnumRegularCardValueList.Queen, EnumSuitList.Diamonds),
                FindCardBySuitValue(EnumRegularCardValueList.King, EnumSuitList.Clubs),
                FindCardBySuitValue(EnumRegularCardValueList.Two, EnumSuitList.Hearts),
                FindCardBySuitValue(EnumRegularCardValueList.Three, EnumSuitList.Spades),
                FindCardBySuitValue(EnumRegularCardValueList.Four, EnumSuitList.Diamonds),
                FindCardBySuitValue(EnumRegularCardValueList.Five, EnumSuitList.Clubs),
                FindCardBySuitValue(EnumRegularCardValueList.Six, EnumSuitList.Hearts),
                FindCardBySuitValue(EnumRegularCardValueList.Seven, EnumSuitList.Spades),
                FindCardBySuitValue(EnumRegularCardValueList.Eight, EnumSuitList.Diamonds),
                FindCardBySuitValue(EnumRegularCardValueList.Nine, EnumSuitList.Clubs)
            };
            CardList!.RemoveGivenList(thisList);
            AfterShuffle(thisList);
        }
    }
}