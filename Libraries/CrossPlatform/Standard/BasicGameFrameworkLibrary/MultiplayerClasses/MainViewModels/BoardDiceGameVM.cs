using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.TestUtilities;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels
{
    public abstract class BoardDiceGameVM : SimpleBoardGameVM
    {
        private readonly IStandardRollProcesses _rollProcesses;

        public BoardDiceGameVM(CommandContainer commandContainer,
            IEndTurn mainGame,
            IDiceBoardGamesData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses rollProcesses
            ) : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _rollProcesses = rollProcesses;
        }
        public virtual bool CanRollDice()
        {
            return true;  //can be false in some cases (?)
        }
        [Command(EnumCommandCategory.Game)]
        public virtual async Task RollDiceAsync() //some games require something else.
        {
            await _rollProcesses.RollDiceAsync();
        }
    }
}