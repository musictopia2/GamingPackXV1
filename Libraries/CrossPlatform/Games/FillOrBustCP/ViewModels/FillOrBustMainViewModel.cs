using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using FillOrBustCP.Cards;
using FillOrBustCP.Data;
using FillOrBustCP.Logic;
using System.Threading.Tasks;
namespace FillOrBustCP.ViewModels
{
    [InstanceGame]
    public class FillOrBustMainViewModel : BasicCardGamesVM<FillOrBustCardInformation>
    {
        private readonly FillOrBustMainGameClass _mainGame;
        private readonly FillOrBustVMData _model;
        public FillOrBustMainViewModel(CommandContainer commandContainer,
            FillOrBustMainGameClass mainGame,
            FillOrBustVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _model.Deck1.NeverAutoDisable = true;
            _model.Cup!.SendEnableProcesses(this, () =>
            {
                return _mainGame!.SaveRoot!.GameStatus == EnumGameStatusList.ChooseDice;
            });
            _model.Cup!.DiceClickedAsync += FillOrBustMainViewModel_DiceClickedAsync;
        }
        private async Task FillOrBustMainViewModel_DiceClickedAsync(SimpleDice arg)
        {
            await _mainGame!.Roller!.SelectUnSelectDiceAsync(arg.Index); // i think
        }
        protected override Task TryCloseAsync()
        {
            _model.Cup!.DiceClickedAsync -= FillOrBustMainViewModel_DiceClickedAsync;
            return base.TryCloseAsync();
        }
        protected override bool CanEnableDeck()
        {
            return _mainGame!.SaveRoot!.GameStatus == EnumGameStatusList.DrawCard ||
                _mainGame.SaveRoot.GameStatus == EnumGameStatusList.ChoosePlay ||
                _mainGame.SaveRoot.GameStatus == EnumGameStatusList.ChooseDraw;
        }
        protected override bool CanEnablePile1()
        {
            return false;
        }
        public override bool CanEndTurn()
        {
            if (_mainGame!.SaveRoot!.GameStatus == EnumGameStatusList.EndTurn ||
                _mainGame.SaveRoot.GameStatus == EnumGameStatusList.ChooseRoll ||
                _mainGame.SaveRoot.GameStatus == EnumGameStatusList.ChooseDraw)
            {
                return true;
            }
            return false;
        }
        protected override async Task ProcessDiscardClickedAsync()
        {
            await Task.CompletedTask;
        }
        public override bool CanEnableAlways()
        {
            return true;
        }

        private int _diceScore;
        [VM]
        public int DiceScore
        {
            get { return _diceScore; }
            set
            {
                if (SetProperty(ref _diceScore, value))
                {
                    
                }
            }
        }
        private int _tempScore;
        [VM]
        public int TempScore
        {
            get { return _tempScore; }
            set
            {
                if (SetProperty(ref _tempScore, value))
                {
                    
                }
            }
        }
        private string _instructions = "";
        [VM]
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    
                }
            }
        }
        public bool CanRollDice()
        {
            if (_mainGame!.SaveRoot!.GameStatus == EnumGameStatusList.RollDice ||
                _mainGame.SaveRoot.GameStatus == EnumGameStatusList.ChooseRoll ||
                _mainGame.SaveRoot.GameStatus == EnumGameStatusList.ChoosePlay)
            {
                return true;
            }
            return false;
        }
        [Command(EnumCommandCategory.Game)]
        public async Task RollDiceAsync()
        {
            await _mainGame!.Roller!.RollDiceAsync();
        }
        public bool CanChooseDice => _mainGame.SaveRoot.GameStatus == EnumGameStatusList.ChooseDice;
        [Command(EnumCommandCategory.Game)]
        public async Task ChooseDiceAsync()
        {
            int score = _mainGame!.CalculateScore();
            if (score == 0)
            {
                ToastPlatform.ShowError("Sorry, you must choose at least one scoring dice");
                return;
            }
            if (_mainGame.BasicData!.MultiPlayer == true)
            {
                await _mainGame.Network!.SendAllAsync("updatescore", score);
            }
            await _mainGame.AddToTempAsync(score);
        }
    }
}