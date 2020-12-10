using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CalculationSolitaireCP.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace CalculationSolitaireCP.Logic
{
    [SingletonGame]
    public class CalculationSolitaireMainGameClass : SolitaireGameClass<CalculationSolitaireSaveInfo>
    {
        public CalculationSolitaireMainGameClass(ISolitaireData solitaireData1,
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
            DeckRegularDict<SolitaireCard> output = new DeckRegularDict<SolitaireCard>();
            4.Times(x =>
            {
                var temps = CardList.Where(items => (int)items.Value == x).ToRegularDeckDict();
                if (temps.Count != 4)
                {
                    throw new BasicBlankException("There must be 4 cards");
                }
                var thisCard = temps.GetRandomItem();
                output.Add(thisCard);
                CardList!.RemoveSpecificItem(thisCard);
            });
            AfterShuffle(output);
            CardList!.Clear();
        }
    }
}