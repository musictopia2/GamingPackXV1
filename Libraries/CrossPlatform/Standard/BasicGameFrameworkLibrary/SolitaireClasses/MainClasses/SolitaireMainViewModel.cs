using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.BasicVMInterfaces;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileInterfaces;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Messenging;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
namespace BasicGameFrameworkLibrary.SolitaireClasses.MainClasses
{
    public abstract class SolitaireMainViewModel<S> : BlazorScreenViewModel,
        IBasicSolitaireVM,
        IBasicEnableProcess,
        IBlankGameVM,
        IAggregatorContainer,
        IHandle<IScoreData>

        where S : SolitaireSavedClass, new()

    {
        private SolitaireGameClass<S>? _mainGame; //attempt this without generics.
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    if (_mainGame!.SaveRoot == null)
                    {
                        return;
                    }
                    _mainGame.SaveRoot.Score = value; //try this.
                }
            }
        }
        public bool CanEnableBasics()
        {
            return true;
        }
        public bool CanStartNewGameImmediately { get; set; } = true;
        public DeckObservablePile<SolitaireCard> DeckPile { get; set; }
        public SingleObservablePile<SolitaireCard> MainDiscardPile { get; set; }
        public IMain MainPiles1 { get; set; }
        public IWaste WastePiles1 { get; set; }
        public CommandContainer CommandContainer { get; set; }
        private readonly BasicData _basicData;
        public SolitaireMainViewModel(IEventAggregator aggregator, CommandContainer command,
            IGamePackageResolver resolver
            )
        {

            CommandContainer = command;
            _resolver = resolver;
            _ = resolver.ReplaceObject<IScoreData>(); //to start over. //try this at the beginning.
            aggregator.Subscribe(this);
            CommandContainer!.ExecutingChanged += CommandContainer_ExecutingChanged;
            DeckPile = resolver.ReplaceObject<DeckObservablePile<SolitaireCard>>();
            DeckPile.DeckClickedAsync += DeckPile_DeckClickedAsync;
            DeckPile.NeverAutoDisable = true;
            _basicData = _resolver.Resolve<BasicData>(); //i think.
            DeckPile.SendEnableProcesses(this, () =>
            {
                if (_mainGame!.GameGoing == false)
                {
                    return false;
                }
                if (_mainGame.DealsRemaining == 0 && DeckPile.IsEndOfDeck())
                {
                    return false;
                }
                else if (_mainGame.NoCardsToShuffle)
                {
                    return false;
                }
                return true;
            });
            MainDiscardPile = new SingleObservablePile<SolitaireCard>(command);
            MainDiscardPile.PileClickedAsync += MainDiscardPile_PileClickedAsync; //we don't have double for now.
            MainPiles1 = resolver.ReplaceObject<IMain>();
            WastePiles1 = resolver.ReplaceObject<IWaste>(); //try this way.
            MainPiles1.PileSelectedAsync += MainPiles1_PileSelectedAsync; //this does not support double for now.
            WastePiles1.PileSelectedAsync += WastePiles1_PileSelectedAsync;
            WastePiles1.DoubleClickAsync += WastePiles1_DoubleClickAsync;
        }

        private async Task DeckPile_DeckClickedAsync()
        {
            await Task.CompletedTask;
            if (DeckPile!.DeckStyle == EnumDeckPileStyle.AlwaysKnown)
            {
                if (WastePiles1!.OneSelected() > 0)
                {
                    return;
                }
                DeckPile.IsSelected = !DeckPile.IsSelected;
                return;
            }
            _mainGame!.DrawCard();
        }

        [Command(EnumCommandCategory.Plain)]
        public async Task AutoMoveAsync()
        {
            await _mainGame!.MakeAutoMovesToMainPilesAsync();
        }
        protected override async Task ActivateAsync()
        {
            _basicData.GameDataLoading = true;
            _mainGame = GetGame(_resolver);
            _mainGame.LinkData(this);
            await _mainGame.InitAsync(this);
            //await Aggregator.SendLoadAsync();
            CommandContainer.ManualReport();
            CommandContainer.UpdateAll();
            _basicData.GameDataLoading = false;
        }
        protected abstract SolitaireGameClass<S> GetGame(IGamePackageResolver resolver);
        private async Task MainPiles1_PileSelectedAsync(int index)
        {
            await _mainGame!.MainPileSelectedAsync(index);
        }
        private bool _didDoubleClick;
        private readonly IGamePackageResolver _resolver;
        private async Task WastePiles1_DoubleClickAsync(int Index)
        {
            _didDoubleClick = true;
            await _mainGame!.WasteToMainAsync(Index);
        }
        private async Task WastePiles1_PileSelectedAsync(int Index)
        {
            if (_didDoubleClick == true)
            {
                _didDoubleClick = false;
                return;
            }
            await _mainGame!.WasteSelectedAsync(Index);
        }
        private async Task MainDiscardPile_PileClickedAsync()
        {
            //no need for double click since its not supported for now.
            await Task.CompletedTask;
            _mainGame!.SelectDiscard();
        }
        private async void CommandContainer_ExecutingChanged()
        {
            if (CommandContainer!.IsExecuting)
            {
                return;
            }
            if (_mainGame == null || _mainGame.SaveRoot == null || _mainGame.GameGoing == false)
            {
                return;
            }
            CommandExecutingChanged(); //this is needed so for games like eagle wings, can determine whether to enable the heel or not (?)
            await _mainGame.SaveGameAsync();//i think
        }
        protected virtual void CommandExecutingChanged() { }
        protected override Task TryCloseAsync()
        {
            CommandContainer.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        void IHandle<IScoreData>.Handle(IScoreData message)
        {
            Score = message.Score;
        }
    }
}