using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using BisleySolitaireCP.Data;
using CommonBasicStandardLibraries.Messenging;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BisleySolitaireCP.Logic
{
    [SingletonGame]
    public class BisleySolitaireMainGameClass : SolitaireGameClass<BisleySolitaireSaveInfo>
    {
        public BisleySolitaireMainGameClass(ISolitaireData solitaireData1,
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
            var aceList = GetAceList();
            AfterShuffle(aceList);
        }
        protected override int WhichAutoMoveIsValid()
        {
            var tempList = _thisMod!.WastePiles1!.GetAllCards();
            for (int x = 0; x < tempList.Count; x++)
            {
                if (_thisMod.WastePiles1.CanSelectUnselectPile(x))
                {
                    var thisCard = tempList[x];
                    if (ValidMainColumn(thisCard) > -1)
                    {
                        return x;
                    }
                }
            }
            return -1;
        }
    }
}