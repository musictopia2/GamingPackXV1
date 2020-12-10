using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using KlondikeSolitaireCP.Data;
namespace KlondikeSolitaireCP.Logic
{
    [SingletonGame]
    public class KlondikeSolitaireMainGameClass : SolitaireGameClass<KlondikeSolitaireSaveInfo>
    {
        public KlondikeSolitaireMainGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            )
            : base(solitaireData1, thisState, aggregator, score, command)
        {
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
    }
}