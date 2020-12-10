using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.ClockClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using ClockSolitaireCP.EventModels;
using ClockSolitaireCP.Logic;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ClockSolitaireCP.ViewModels
{
    [InstanceGame]
    public class ClockSolitaireMainViewModel : BlazorScreenViewModel,
        IBasicEnableProcess,
        IBlankGameVM,
        IAggregatorContainer,
        IClockVM,
        IHandle<CardsLeftEventModel>
    {
        private readonly IEventAggregator _aggregator;
        private readonly BasicData _basicData;
        private readonly ClockSolitaireMainGameClass _mainGame;
        private int _cardsLeft;
        public int CardsLeft
        {
            get { return _cardsLeft; }
            set
            {
                if (SetProperty(ref _cardsLeft, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
        public DeckObservablePile<SolitaireCard> DeckPile { get; set; }
        public ClockBoard? Clock1;
        public ClockSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer commandContainer,
            IGamePackageResolver resolver,
            BasicData basicData
            )
        {
            _aggregator = aggregator;
            CommandContainer = commandContainer;
            _basicData = basicData;
            CommandContainer.ExecutingChanged += CommandContainer_ExecutingChanged; //hopefully no problem (?)
            DeckPile = resolver.ReplaceObject<DeckObservablePile<SolitaireCard>>();
            DeckPile.DeckClickedAsync += DeckPile_DeckClickedAsync;
            DeckPile.NeverAutoDisable = true;
            DeckPile.SendEnableProcesses(this, () =>
            {
                return false; //i think.
            });
            _mainGame = resolver.ReplaceObject<ClockSolitaireMainGameClass>(); //hopefully this works.  means you have to really rethink.
            Clock1 = new ClockBoard(this, _mainGame, commandContainer, resolver, _aggregator);
            _aggregator.Subscribe(this);
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
            await _mainGame.NewGameAsync(this);
            CommandContainer.UpdateAll();
            _basicData.GameDataLoading = false;
        }
        protected override Task TryCloseAsync()
        {
            CommandContainer.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        async Task IClockVM.ClockClickedAsync(int index)
        {
            if (Clock1!.IsValidMove(index) == false)
            {
                await UIPlatform.ShowMessageAsync("Illegal Move");
                return;
            }
            Clock1.MakeMove(index);
            if (Clock1.HasWon())
            {
                await _mainGame!.ShowWinAsync();
                return;
            }
            if (Clock1.IsGameOver())
            {
                await _mainGame!.ShowLossAsync();
            }
        }
        void IHandle<CardsLeftEventModel>.Handle(CardsLeftEventModel message)
        {
            CardsLeft = message.CardsLeft;
        }
    }
}