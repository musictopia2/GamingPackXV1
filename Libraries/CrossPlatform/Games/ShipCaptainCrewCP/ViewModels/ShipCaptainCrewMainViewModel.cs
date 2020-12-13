using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using ShipCaptainCrewCP.Data;
using ShipCaptainCrewCP.Logic;
namespace ShipCaptainCrewCP.ViewModels
{
    [InstanceGame]
    public class ShipCaptainCrewMainViewModel : DiceGamesVM<SimpleDice>
    {
        private readonly ShipCaptainCrewMainGameClass _mainGame;
        private readonly ShipCaptainCrewVMData _model;
        public ShipCaptainCrewMainViewModel(CommandContainer commandContainer,
            ShipCaptainCrewMainGameClass mainGame,
            ShipCaptainCrewVMData viewModel,
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
        protected override bool NeedsRollIncrement => false;
        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        public PlayerCollection<ShipCaptainCrewPlayerItem> PlayerList => _mainGame.PlayerList;
        protected override bool CanEnableDice()
        {
            return true;
        }
        public override bool CanEndTurn()
        {
            return false;
        }
        public override bool CanRollDice()
        {
            return true;
        }
    }
}