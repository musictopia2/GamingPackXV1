using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using VegasSolitaireCP.Data;
namespace VegasSolitaireCP.Logic
{
    [SingletonGame]
    public class VegasSolitaireMainGameClass : SolitaireGameClass<VegasSolitaireSaveInfo>
    {
        public VegasSolitaireMainGameClass(ISolitaireData solitaireData1,
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
        internal Action? AddMoney { get; set; }
        internal Action? ResetMoney { get; set; }
        protected override void AfterShuffleCards()
        {
            _thisMod!.MainPiles1!.ClearBoard();
            ResetMoney!.Invoke();
            AfterShuffle();
        }
        protected override void AddToScore()
        {
            AddMoney!.Invoke();
        }
    }
}