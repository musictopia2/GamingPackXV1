using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using BasicGameFrameworkLibrary.TestUtilities;
using System.Threading.Tasks;

namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels
{
    public class YahtzeeMainViewModel<D> : DiceGamesVM<D>
        where D : SimpleDice, new()
    {
        private YahtzeeVMData<D> _vmData;
        public YahtzeeMainViewModel(
            CommandContainer commandContainer,
            IHoldUnholdProcesses mainGame,
            YahtzeeVMData<D> viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses rollProcesses,
            YahtzeeGameContainer<D> gameContainer)
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver, rollProcesses)
        {
            _resolver = resolver;
            _gameContainer = gameContainer;
            _gameContainer.GetNewScoreAsync = LoadNewScoreAsync;
            _vmData = viewModel;
        }
        protected override bool NeedsRollIncrement => false;
        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            await LoadNewScoreAsync();
        }
        public YahtzeeScoresheetViewModel<D>? CurrentScoresheet { get; set; }

        private async Task LoadNewScoreAsync()
        {
            if (CurrentScoresheet != null)
            {
                await CloseSpecificChildAsync(CurrentScoresheet);
            }
            CurrentScoresheet = _resolver.Resolve<YahtzeeScoresheetViewModel<D>>();
            await LoadScreenAsync(CurrentScoresheet);
        }
        public override bool CanRollDice()
        {
            return RollNumber <= 3;
        }
        protected override bool CanEnableDice()
        {
            return CanRollDice();
        }
        private int _round;
        private readonly IGamePackageResolver _resolver;
        private readonly YahtzeeGameContainer<D> _gameContainer;

        [VM]
        public int Round
        {
            get { return _round; }
            set
            {
                if (SetProperty(ref _round, value)) { }
            }
        }

        public PlayerCollection<YahtzeePlayerItem<D>> PlayerList => _gameContainer.PlayerList!;
        public DiceCup<D> GetCup => _vmData.Cup!;
    }
}