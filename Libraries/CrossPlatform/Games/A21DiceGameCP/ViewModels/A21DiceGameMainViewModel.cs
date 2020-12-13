using A21DiceGameCP.Data;
using A21DiceGameCP.Logic;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
namespace A21DiceGameCP.ViewModels
{
    [InstanceGame]
    public class A21DiceGameMainViewModel : DiceGamesVM<SimpleDice>
    {
        private readonly A21DiceGameMainGameClass _mainGame;
        private readonly A21DiceGameVMData _model;
        public A21DiceGameMainViewModel(CommandContainer commandContainer,
            A21DiceGameMainGameClass mainGame,
            A21DiceGameVMData viewModel,
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
        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        public PlayerCollection<A21DiceGamePlayerItem> PlayerList => _mainGame.PlayerList;
        protected override bool CanEnableDice()
        {
            return false;
        }
        public override bool CanEndTurn()
        {
            return _mainGame!.SingleInfo!.NumberOfRolls > 0;
        }
        public override bool CanRollDice()
        {
            return base.CanRollDice();
        }
    }
}