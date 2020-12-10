using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.TriangleClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using TriangleSolitaireCP.Data;
using TriangleSolitaireCP.Logic;
namespace TriangleSolitaireCP.ViewModels
{
    [InstanceGame]
    public class TriangleSolitaireMainViewModel : BlazorScreenViewModel,
        IBasicEnableProcess,
        IBlankGameVM,
        IAggregatorContainer,
        ITriangleVM
    {
        private readonly IEventAggregator _aggregator;
        private readonly BasicData _basicData;
        private readonly TriangleSolitaireMainGameClass _mainGame;
        public DeckObservablePile<SolitaireCard> DeckPile { get; set; }
        public SingleObservablePile<SolitaireCard> Pile1;
        public TriangleBoard Triangle1;
        public TriangleSolitaireMainViewModel(IEventAggregator aggregator,
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
                if (_mainGame!.GameGoing == false)
                {
                    return false;
                }
                return true; //if other logic is needed for deck, put here.
            });
            _mainGame = resolver.ReplaceObject<TriangleSolitaireMainGameClass>(); //hopefully this works.  means you have to really rethink.
            Pile1 = new SingleObservablePile<SolitaireCard>(commandContainer);
            Triangle1 = new TriangleBoard(this, CommandContainer, resolver);
            Pile1.Text = "Discard";
            Pile1.SendEnableProcesses(this, () => false);
        }
        private async Task DeckPile_DeckClickedAsync()
        {
            if (DeckPile!.IsEndOfDeck())
            {
                ToastPlatform.ShowInfo($"You left {Triangle1!.HowManyCardsLeft} cards");
                await Task.Delay(2000);
                _mainGame.GameGoing = false;
                await _mainGame.SendGameOverAsync();
                return;
            }
            _mainGame!.DrawCard(this);
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
            _basicData.GameDataLoading = false;
            await base.ActivateAsync();
            Pile1.ClearCards();
            _mainGame.InitDraw = (() =>
            {
                var newList = DeckPile!.DrawSeveralCards(15);
                Triangle1!.ClearCards(newList);
                _mainGame.DrawCard(this);
            });
            await _mainGame.NewGameAsync(DeckPile);
            DeckPile.IsEnabled = true; //has to be done manually.
        }
        protected override Task TryCloseAsync()
        {
            CommandContainer.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        async Task ITriangleVM.CardClickedAsync(SolitaireCard thisCard)
        {
            var pileCard = Pile1!.GetCardInfo();
            if (_mainGame!.IsValidMove(thisCard, pileCard, out EnumIncreaseList tempi) == false)
            {
                ToastPlatform.ShowError("Sorry, wrong card");
                return;
            }
            await _mainGame.MakeMoveAsync(thisCard.Deck, tempi, this);
        }
    }
}