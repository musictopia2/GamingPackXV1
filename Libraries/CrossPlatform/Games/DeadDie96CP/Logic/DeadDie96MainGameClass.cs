using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using DeadDie96CP.Data;
using System.Linq;
using System.Threading.Tasks;
namespace DeadDie96CP.Logic
{
    [SingletonGame]
    public class DeadDie96MainGameClass : DiceGameClass<TenSidedDice, DeadDie96PlayerItem, DeadDie96SaveInfo>
    {
        private readonly DeadDie96VMData _model;
        private readonly StandardRollProcesses<TenSidedDice, DeadDie96PlayerItem> _roller;
        public DeadDie96MainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            DeadDie96VMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            BasicGameContainer<DeadDie96PlayerItem, DeadDie96SaveInfo> gameContainer,
            StandardRollProcesses<TenSidedDice, DeadDie96PlayerItem> roller) :
            base(mainContainer, aggregator, basicData, test, currentMod, state, delay, command, gameContainer, roller)
        {
            _model = currentMod;
            _roller = roller;
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            AfterRestoreDice();
            return Task.CompletedTask;
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            LoadMod();
            IsLoaded = true;
        }
        protected override async Task ComputerTurnAsync()
        {
            if (Test!.NoAnimations == false)
            {
                await Delay!.DelayMilli(200);
            }
            await _roller!.RollDiceAsync();
        }
        private async Task GameOverAsync()
        {
            SingleInfo = PlayerList.OrderBy(Items => Items.TotalScore).Take(1).Single();
            await ShowWinAsync();
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            SetUpDice();
            PlayerList!.ForEach(x =>
            {
                x.TotalScore = 0;
                x.CurrentScore = 0;
            });
            SaveRoot!.ImmediatelyStartTurn = true;
            await FinishUpAsync(isBeginning);
        }
        public override async Task StartNewTurnAsync()
        {
            PrepStartTurn();
            _model!.Cup!.HowManyDice = 5;
            _model.Cup.HideDice();
            _model.Cup.CanShowDice = false;
            ProtectedStartTurn();
            await ContinueTurnAsync();
        }
        protected override async Task ProtectedAfterRollingAsync()
        {
            var thisList = _model!.Cup!.DiceList.ToCustomBasicList();
            if (thisList.Any(x => x.Value == 6 || x.Value == 9))
            {
                SingleInfo!.CurrentScore = 0;
                if (Test!.NoAnimations == false)
                {
                    await Delay!.DelayMilli(500);
                }
                _model.Cup.RemoveConditionalDice(Items => Items.Value == 6 || Items.Value == 9);
                if (_model.Cup.DiceList.Count() == 0 || Test.ImmediatelyEndGame)
                {
                    await EndTurnAsync();
                    return;
                }
                await ContinueTurnAsync();
                return;
            }
            int totalScore = _model.Cup.DiceList.Sum(Items => Items.Value);
            SingleInfo!.CurrentScore = totalScore;
            SingleInfo.TotalScore += totalScore;
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            if (WhoTurn == WhoStarts)
            {
                await GameOverAsync();
                return;
            }
            await StartNewTurnAsync();
        }
    }
}