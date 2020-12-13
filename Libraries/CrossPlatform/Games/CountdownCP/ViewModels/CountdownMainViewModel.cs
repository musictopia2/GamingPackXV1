using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CountdownCP.Data;
using CountdownCP.Logic;
namespace CountdownCP.ViewModels
{
    [InstanceGame]
    public class CountdownMainViewModel : DiceGamesVM<CountdownDice>
    {
        private readonly CountdownMainGameClass _mainGame;
        private readonly CountdownVMData _model;
        public CountdownMainViewModel(CommandContainer commandContainer,
            CountdownMainGameClass mainGame,
            CountdownVMData viewModel,
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
        public DiceCup<CountdownDice> GetCup => _model.Cup!;
        public PlayerCollection<CountdownPlayerItem> PlayerList => _mainGame.PlayerList;
        protected override bool CanEnableDice()
        {
            return false;
        }
        public override bool CanEndTurn()
        {
            return true;
        }
        public override bool CanRollDice()
        {
            return false;
        }
        private int _round; //this is needed because the game has to end at some point no matter what even if tie.
        [VM]
        public int Round
        {
            get { return _round; }
            set
            {
                if (SetProperty(ref _round, value))
                {

                }
            }
        }
        [Command(EnumCommandCategory.Game)]
        public void Hint()
        {
            CountdownVMData.ShowHints = true;
        }
    }
}