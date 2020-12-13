using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using System.Threading.Tasks;
using TroubleCP.Data;
using TroubleCP.Logic;
namespace TroubleCP.ViewModels
{
    [InstanceGame]
    public class TroubleMainViewModel : BoardDiceGameVM
    {
        private readonly TroubleMainGameClass _mainGame;
        private readonly TroubleVMData _model;
        public TroubleMainViewModel(CommandContainer commandContainer,
            TroubleMainGameClass mainGame,
            TroubleVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller,
            TroubleGameContainer gameContainer
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = model;
            gameContainer.CanRollDice = CanRollDice;
            gameContainer.RollDiceAsync = RollDiceAsync;
        }
        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        public override bool CanRollDice()
        {
            return _mainGame.SaveRoot.DiceNumber == 0;
        }
        public override async Task RollDiceAsync()
        {
            await base.RollDiceAsync();
        }
    }
}