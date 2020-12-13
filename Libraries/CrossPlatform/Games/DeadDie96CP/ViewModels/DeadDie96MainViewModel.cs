using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using DeadDie96CP.Data;
using DeadDie96CP.Logic;
namespace DeadDie96CP.ViewModels
{
    [InstanceGame]
    public class DeadDie96MainViewModel : DiceGamesVM<TenSidedDice>
    {
        private readonly DeadDie96MainGameClass _mainGame; //if we don't need, delete.
        private readonly DeadDie96VMData _model;
        public DeadDie96MainViewModel(CommandContainer commandContainer,
            DeadDie96MainGameClass mainGame,
            DeadDie96VMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = viewModel;
        }
        public DiceCup<TenSidedDice> GetCup => _model.Cup!;
        public PlayerCollection<DeadDie96PlayerItem> PlayerList => _mainGame.PlayerList;
        protected override bool CanEnableDice()
        {
            return false;
        }
        public override bool CanEndTurn()
        {
            return false;
        }
        public override bool CanRollDice()
        {
            return base.CanRollDice();
        }
    }
}