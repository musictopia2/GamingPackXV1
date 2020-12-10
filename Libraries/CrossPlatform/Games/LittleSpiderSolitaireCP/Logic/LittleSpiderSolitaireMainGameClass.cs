using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using LittleSpiderSolitaireCP.Data;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace LittleSpiderSolitaireCP.Logic
{
    [SingletonGame]
    public class LittleSpiderSolitaireMainGameClass : SolitaireGameClass<LittleSpiderSolitaireSaveInfo>
    {
        public LittleSpiderSolitaireMainGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            )
            : base(solitaireData1, thisState, aggregator, score, command)
        {
        }
        private WastePiles? _thisWaste;
        public override Task NewGameAsync()
        {
            if (_thisWaste == null)
            {
                _thisWaste = (WastePiles)_thisMod!.WastePiles1;
            }
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
        public override void DrawCard()
        {
            var thisList = _thisMod!.DeckPile!.DrawSeveralCards(8);
            _thisWaste!.AddCards(thisList);
        }
        protected override void AfterShuffleCards()
        {
            var firstKing = CardList.First(items => items.Value == EnumRegularCardValueList.King);
            var thisList = new DeckRegularDict<SolitaireCard>
            {
                firstKing
            };
            CardList!.RemoveSpecificItem(firstKing);
            var nextKing = CardList.First(items => items.Value == EnumRegularCardValueList.King && items.Color == firstKing.Color);
            CardList.RemoveSpecificItem(nextKing);
            thisList.Add(nextKing);
            var nextList = CardList.Where(items => items.Value == EnumRegularCardValueList.LowAce && items.Color != firstKing.Color).ToRegularDeckDict();
            CardList.RemoveGivenList(nextList);
            thisList.AddRange(nextList);
            if (thisList.Count != 4)
            {
                throw new BasicBlankException("Must have 4 cards for foundation");
            }
            AfterShuffle(thisList);
            CardList.Clear();
        }
    }
}