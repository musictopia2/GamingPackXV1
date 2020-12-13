using AggravationCP.Data;
using AggravationCP.Logic;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using System.Threading.Tasks;
namespace AggravationCP.ViewModels
{
    [InstanceGame]
    public class AggravationMainViewModel : BoardDiceGameVM
    {
        private readonly AggravationMainGameClass _mainGame;
        private readonly AggravationVMData _model;
        public AggravationMainViewModel(CommandContainer commandContainer,
            AggravationMainGameClass mainGame,
            AggravationVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = model;
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