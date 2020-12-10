using AccordianSolitaireCP.Data;
using AccordianSolitaireCP.EventModels;
using AccordianSolitaireCP.Logic;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace AccordianSolitaireCP.ViewModels
{
    [InstanceGame]
    public class AccordianSolitaireMainViewModel : BlazorScreenViewModel,
        IBasicEnableProcess,
        IBlankGameVM,
        IAggregatorContainer,
        IHandle<ScoreEventModel>
    {
        private readonly IEventAggregator _aggregator;
        private readonly BasicData _basicData;
        private readonly AccordianSolitaireMainGameClass _mainGame;

        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    //can decide what to do when property changes
                }

            }
        }


        public DeckObservablePile<AccordianSolitaireCardInfo> DeckPile { get; set; }
        public GameBoard GameBoard1;

        public void UnselectAll()
        {
            GameBoard1.UnselectAllObjects();
        }

        public AccordianSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer commandContainer,
            IGamePackageResolver resolver,
            BasicData basicData
            )
        {
            _aggregator = aggregator;
            CommandContainer = commandContainer;
            _basicData = basicData;
            CommandContainer.ExecutingChanged += CommandContainer_ExecutingChanged; //hopefully no problem (?)
            DeckPile = resolver.ReplaceObject<DeckObservablePile<AccordianSolitaireCardInfo>>();
            DeckPile.DeckClickedAsync += DeckPile_DeckClickedAsync;
            DeckPile.NeverAutoDisable = true;
            DeckPile.SendEnableProcesses(this, () =>
            {
                return false;
            });
            GameBoard1 = new GameBoard(commandContainer);
            GameBoard1.ObjectClickedAsync += GameBoard1_ObjectClickedAsync;
            _aggregator.Subscribe(this);
            _mainGame = resolver.ReplaceObject<AccordianSolitaireMainGameClass>(); //hopefully this works.  means you have to really rethink.
        }
        private async Task GameBoard1_ObjectClickedAsync(AccordianSolitaireCardInfo card, int index)
        {
            if (index == -1)
            {
                throw new BasicBlankException("Index cannot be -1.  Rethink");
            }
            if (GameBoard1!.IsCardSelected(card) == false)
            {
                GameBoard1.SelectUnselectCard(card);
                return;
            }
            if (GameBoard1.IsValidMove(card) == false)
            {
                await UIPlatform.ShowMessageAsync("Illegal Move");
                return;
            }
            GameBoard1.MakeMove(card);
            if (Score == 52)
            {
                await _mainGame!.ShowWinAsync();
            }
        }
        private async Task DeckPile_DeckClickedAsync()
        {
            await Task.CompletedTask;
        }

        private async void CommandContainer_ExecutingChanged()
        {
            if (CommandContainer.IsExecuting)
            {
                return;
            }
            if (_mainGame.GameGoing)
            {
                await _mainGame.SaveStateAsync();
            }
        }
        protected override Task TryCloseAsync()
        {
            CommandContainer.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        public CommandContainer CommandContainer { get; set; }
        IEventAggregator IAggregatorContainer.Aggregator => _aggregator;
        public bool CanEnableBasics()
        {
            return true; //because maybe you can't enable it.
        }
        protected override async Task ActivateAsync()
        {
            _basicData.GameDataLoading = true;
            await base.ActivateAsync();
            await _mainGame.NewGameAsync(DeckPile, GameBoard1);
            GameBoard1.IsEnabled = true; //try this way.
            _basicData.GameDataLoading = false;
        }
        void IHandle<ScoreEventModel>.Handle(ScoreEventModel message)
        {
            Score = message.Score;
        }
    }
}