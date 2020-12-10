using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using CribbagePatienceCP.Data;
using CribbagePatienceCP.EventModels;
using CribbagePatienceCP.Logic;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace CribbagePatienceCP.ViewModels
{
    [InstanceGame]
    public class CribbagePatienceMainViewModel : BlazorScreenViewModel,
        IBasicEnableProcess,
        IBlankGameVM,
        IAggregatorContainer,
        IHandle<HandScoresEventModel>

    {
        private readonly IEventAggregator _aggregator;
        private readonly BasicData _basicData;
        private readonly CribbagePatienceMainGameClass _mainGame;
        public SingleObservablePile<CribbageCard>? StartPile;
        public HandObservable<CribbageCard> TempCrib;
        public HandObservable<CribbageCard> Hand1;
        public CustomBasicList<ScoreHandCP>? HandScores;
        public ScoreSummaryCP? Scores;
        public ScoreHandCP GetScoreHand(EnumHandCategory thisCategory)
        {
            return HandScores!.Single(items => items.HandCategory == thisCategory);
        }
        public (int row, int column) GetRowColumn(EnumHandCategory thisCategory)
        {
            var hand = GetScoreHand(thisCategory);
            return hand.GetRowColumn();
        }
        public DeckObservablePile<CribbageCard> DeckPile { get; set; }
        public bool CanCrib => Hand1.Visible;
        [Command(EnumCommandCategory.Plain)]
        public async Task CribAsync()
        {
            int manys = Hand1.HowManySelectedObjects;
            if (manys == 0)
            {
                ToastPlatform.ShowError("Must choose cards");
                return;
            }
            if (manys != 2)
            {
                ToastPlatform.ShowError("Must choose 2 cards for crib");
                return;
            }
            var thisList = Hand1.ListSelectedObjects(true);
            if (Hand1.HandList.Count == 6)
            {
                throw new BasicBlankException("Did not remove cards before starting to put to crib");
            }
            _mainGame.RemoveTempCards(thisList);
            _mainGame.CardsToCrib(thisList);
            if (DeckPile.IsEndOfDeck())
            {
                await UIPlatform.ShowMessageAsync("Game Over.  Check Results");
                //this time, go ahead and do this way so i can check results.
                //otherwise, would have a button to click on to end game.
                //ToastPlatform.ShowInfo("Game Over.  Check Results");
                //await Task.Delay(2000);
                await _mainGame.SendGameOverAsync();
            }
        }
        public bool CanContinue()
        {
            if (Hand1.Visible == true)
            {
                return false;
            }
            return !DeckPile.IsEndOfDeck();
        }

        [Command(EnumCommandCategory.Plain)]
        public void Continue()
        {
            _mainGame.NewRound();
        }
        public CribbagePatienceMainViewModel(IEventAggregator aggregator,
            CommandContainer commandContainer,
            IGamePackageResolver resolver,
            BasicData basicData
            )
        {
            _aggregator = aggregator;
            CommandContainer = commandContainer;
            _basicData = basicData;
            CommandContainer.ExecutingChanged += CommandContainer_ExecutingChanged; //hopefully no problem (?)
            DeckPile = resolver.ReplaceObject<DeckObservablePile<CribbageCard>>();
            DeckPile.DeckClickedAsync += DeckPile_DeckClickedAsync;
            DeckPile.NeverAutoDisable = false;
            DeckPile.SendEnableProcesses(this, () =>
            {
                return false;
            });

            Hand1 = new HandObservable<CribbageCard>(commandContainer);
            Hand1.Visible = false; //has to be proven true.
            Hand1.Maximum = 6;
            Hand1.AutoSelect =EnumHandAutoType.SelectAsMany;
            _mainGame = resolver.ReplaceObject<CribbagePatienceMainGameClass>(); //hopefully this works.  means you have to really rethink.
            _aggregator.Subscribe(this);
            _mainGame._saveRoot.HandScores = new CustomBasicList<ScoreHandCP>();
            3.Times(x =>
            {
                var tempHand = new ScoreHandCP();
                tempHand.HandCategory = (EnumHandCategory)x;
                _mainGame._saveRoot.HandScores.Add(tempHand);
            });
            StartPile = new SingleObservablePile<CribbageCard>(CommandContainer);
            StartPile.Text = "Start Card";
            StartPile.CurrentOnly = true;
            StartPile.SendEnableProcesses(this, () => false);
            Scores = new ScoreSummaryCP();
            TempCrib = new HandObservable<CribbageCard>(CommandContainer);
            TempCrib.Visible = false;
            TempCrib.Text = "Crib So Far";
            TempCrib.Maximum = 4;
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
            _basicData.GameDataLoading = false;
            await base.ActivateAsync();
            await _mainGame.NewGameAsync(this);
            Hand1.IsEnabled = true; //somehow a timing issue.  this should fix this one.  not sure if its going to be more of a problem later or not (?)
            CommandContainer.UpdateAll();
        }
        protected override Task TryCloseAsync()
        {
            CommandContainer.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        void IHandle<HandScoresEventModel>.Handle(HandScoresEventModel message)
        {
            HandScores = message.HandScores;
        }
    }
}