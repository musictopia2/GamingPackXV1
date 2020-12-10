using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using FlorentineSolitaireCP.Data;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace FlorentineSolitaireCP.Logic
{
    [SingletonGame]
    public class FlorentineSolitaireMainGameClass : SolitaireGameClass<FlorentineSolitaireSaveInfo>
    {
        public FlorentineSolitaireMainGameClass(ISolitaireData solitaireData1,
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
            DeckRegularDict<SolitaireCard> thisList = new DeckRegularDict<SolitaireCard>
            {
                CardList![5]
            };
            CardList.RemoveAt(5);
            AfterShuffle(thisList);
        }
    }
}