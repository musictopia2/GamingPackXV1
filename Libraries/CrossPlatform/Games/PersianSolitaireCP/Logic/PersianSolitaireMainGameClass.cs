using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using PersianSolitaireCP.Data;
using PersianSolitaireCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace PersianSolitaireCP.Logic
{
    [SingletonGame]
    public class PersianSolitaireMainGameClass : SolitaireGameClass<PersianSolitaireSaveInfo>, IHandle<ScoreModel>
    {
        readonly ScoreModel _model;
        public PersianSolitaireMainGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            )
            : base(solitaireData1, thisState, aggregator, score, command)
        {
            _model = (ScoreModel)score;
            aggregator.Subscribe(this);
        }
        protected async override Task ContinueOpenSavedAsync()
        {
            _model.DealNumber = SaveRoot.DealNumber;
            await base.ContinueOpenSavedAsync();
        }
        protected async override Task FinishSaveAsync()
        {
            if (SaveRoot.DealNumber == 0)
            {
                throw new BasicBlankException("The deal cannot be 0.  Rethink");
            }
            await base.FinishSaveAsync();
        }
        protected override SolitaireCard CardPlayed()
        {
            var thisCard = base.CardPlayed();
            return thisCard;
        }
        private PersianSolitaireMainViewModel? _newVM;
        protected override void AfterShuffleCards()
        {
            _model.DealNumber = 1;
            _thisMod!.MainPiles1!.ClearBoard();
            AfterShuffle();
        }

        void IHandle<ScoreModel>.Handle(ScoreModel message)
        {
            if (_newVM == null)
            {
                _newVM = (PersianSolitaireMainViewModel)_thisMod!;
            }
            _newVM.DealNumber = message.DealNumber;
            SolitairePilesCP.DealNumber = _model.DealNumber;
            SaveRoot.DealNumber = message.DealNumber; //hopefully this simple.
        }
    }
}