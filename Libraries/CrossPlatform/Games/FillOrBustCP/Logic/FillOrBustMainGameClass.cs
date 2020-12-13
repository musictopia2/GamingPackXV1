using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using FillOrBustCP.Cards;
using FillOrBustCP.Data;
using System.Linq;
using System.Threading.Tasks;
namespace FillOrBustCP.Logic
{
    [SingletonGame]
    public class FillOrBustMainGameClass : CardGameClass<FillOrBustCardInformation, FillOrBustPlayerItem, FillOrBustSaveInfo>, IMiscDataNM, IFinishStart
    {
        private readonly FillOrBustVMData _model;
        private readonly CommandContainer _command;
        public StandardRollProcesses<SimpleDice, FillOrBustPlayerItem> Roller;
        public FillOrBustMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            FillOrBustVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<FillOrBustCardInformation> cardInfo,
            CommandContainer command,
            FillOrBustGameContainer gameContainer,
            StandardRollProcesses<SimpleDice, FillOrBustPlayerItem> roller
            )
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
            Roller = roller;
            Roller.AfterRollingAsync = AfterRollingAsync;
            Roller.AfterSelectUnselectDiceAsync = AfterSelectUnselectDiceAsync;
            Roller.CurrentPlayer = (() => SingleInfo!);
        }
        protected override async Task AfterDrawingAsync()
        {
            _command.UpdateAll();
            var thisCard = _model!.Pile1!.GetCardInfo();
            if (thisCard.IsOptional == true)
            {
                if (CanPlayRevenge() == true)
                {
                    SaveRoot!.GameStatus = EnumGameStatusList.ChoosePlay;
                }
                else
                {
                    SaveRoot!.GameStatus = EnumGameStatusList.DrawCard;
                }
            }
            else if (thisCard.Status == EnumCardStatusList.NoDice)
            {
                SaveRoot!.TempScore = 0;
                SaveRoot.DiceScore = 0;
                SaveRoot.GameStatus = EnumGameStatusList.EndTurn;
            }
            else
            {
                SaveRoot!.GameStatus = EnumGameStatusList.RollDice;
                SaveRoot.FillsRequired = thisCard.FillsRequired;
            }
            if (SaveRoot.GameStatus != EnumGameStatusList.DrawCard && SaveRoot.GameStatus != EnumGameStatusList.EndTurn)
            {
                _model.Cup!.HowManyDice = 6;
            }
            await ContinueTurnAsync();
        }
        public async Task AfterRollingAsync()
        {
            if (SaveRoot!.GameStatus == EnumGameStatusList.ChoosePlay)
            {
                SaveRoot.FillsRequired = 1; //because you chose vengence.
                var thisList = WinList();
                thisList.ForEach(items =>
                {
                    FillOrBustPlayerItem thisPlayer = PlayerList![items];
                    int points = thisPlayer.TotalScore;
                    if (points < 2500)
                    {
                        points = 0;
                    }
                    else
                    {
                        points -= 2500;
                    }
                    thisPlayer.TotalScore = points;
                });
            }
            await ProcessScoresAsync();
            await ContinueTurnAsync();
        }
        public async Task AfterSelectUnselectDiceAsync()
        {
            await ContinueTurnAsync();
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            SaveRoot!.LoadMod(_model!);
            _model!.LoadCup(SaveRoot, true);
            SaveRoot.DiceList.MainContainer = MainContainer;
            return base.FinishGetSavedAsync();
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            IsLoaded = true;
        }
        protected override async Task ComputerTurnAsync()
        {
            await Task.CompletedTask;
        }
        protected override Task StartSetUpAsync(bool isBeginning)
        {
            if (IsLoaded == false)
            {
                SaveRoot!.LoadMod(_model!);
                _model!.LoadCup(SaveRoot, false);
                SaveRoot.DiceList.MainContainer = MainContainer;
            }
            LoadControls();
            if (isBeginning == false)
            {
                PlayerList!.ForEach(items =>
                {
                    items.TotalScore = 0;
                    items.CurrentScore = 0;
                });
            }
            SaveRoot!.GameStatus = EnumGameStatusList.DrawCard;
            return base.StartSetUpAsync(isBeginning);
        }
        public override Task ContinueTurnAsync()
        {
            _model!.Instructions = SaveRoot!.GameStatus switch
            {
                EnumGameStatusList.DrawCard => "Draw a card",
                EnumGameStatusList.EndTurn => "Need to end your turn",
                EnumGameStatusList.RollDice => "Roll the dice",
                EnumGameStatusList.ChoosePlay => "Either play the vengeance or draw again",
                EnumGameStatusList.ChooseDice => "Choose at least one scoring dice to remove",
                EnumGameStatusList.ChooseDraw => "Either draw a card or end your turn",
                EnumGameStatusList.ChooseRoll => "Either roll the dice to get more points or end your turn to keep your existing score this round",
                _ => throw new BasicBlankException("No status"),
            };
            return base.ContinueTurnAsync();
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "choosedice":
                    await Roller!.SelectUnSelectDiceAsync(int.Parse(content));
                    break;
                case "updatescore":
                    await AddToTempAsync(int.Parse(content));
                    break;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public Task FinishStartAsync()
        {
            if (SaveRoot!.GameStatus != EnumGameStatusList.RollDice)
            {
                _model!.Cup!.HowManyDice = _model!.Cup.DiceList.Count();
                _model.Cup.CanShowDice = true;
                _model.Cup.Visible = true;
            }
            else
            {
                _model.Cup!.HowManyDice = 6;
                _model.Cup.Visible = false;
                _model.Cup.CanShowDice = false;
            }
            return Task.CompletedTask;
        }
        public override async Task StartNewTurnAsync()
        {
            await base.StartNewTurnAsync();
            SaveRoot!.GameStatus = EnumGameStatusList.DrawCard;
            SingleInfo!.CurrentScore = 0;
            _model.Cup!.CanShowDice = false;
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            TransferScore();
            SingleInfo!.TotalScore += SingleInfo.CurrentScore;
            _command.ManuelFinish = true;
            if (SingleInfo.TotalScore < 5000)
            {
                WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
                await StartNewTurnAsync();
                return;
            }
            var ThisList = WinList();
            if (ThisList.Count > 1)
            {
                WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
                await StartNewTurnAsync();
                return;
            }
            SingleInfo = PlayerList![ThisList.Single()];
            await ShowWinAsync();
        }
        #region Unique Game Features
        private async Task ProcessScoresAsync()
        {
            var tempList = _model!.Cup!.DiceList.ToCustomBasicList();
            int nums;
            nums = CalculateScore(tempList, true, out bool Fills);
            var thisCard = _model.Pile1!.GetCardInfo();
            if (nums == 0)
            {
                if (thisCard.Status != EnumCardStatusList.MustBust)
                {
                    SaveRoot!.TempScore = 0;
                }
                SaveRoot!.GameStatus = EnumGameStatusList.EndTurn;
            }
            else
            {
                if (Fills == true)
                {
                    SaveRoot!.TempScore += nums;
                    if (thisCard.Status == EnumCardStatusList.MustBust)
                    {
                        if (Test!.NoAnimations == false)
                        {
                            await Delay!.DelaySeconds(2);
                        }
                        _model.Cup.HowManyDice = 6;
                        _model.Cup.CanShowDice = false;
                        SaveRoot.GameStatus = EnumGameStatusList.RollDice;
                    }
                    else
                    {
                        SaveRoot.TempScore += thisCard.BonusAmount;
                        if (SaveRoot.FillsRequired > 0)
                        {
                            SaveRoot.FillsRequired--;
                        }
                        if (SaveRoot.FillsRequired == 0)
                        {
                            if (thisCard.AddToScore == true)
                            {
                                if (thisCard.Status == EnumCardStatusList.DoubleTrouble)
                                {
                                    SaveRoot.TempScore *= 2;
                                }
                                TransferScore();
                                SaveRoot.GameStatus = EnumGameStatusList.DrawCard;
                            }
                            else
                            {
                                SaveRoot.GameStatus = EnumGameStatusList.ChooseDraw;
                            }
                        }
                        else
                        {
                            if (Test!.NoAnimations == false)
                            {
                                await Delay!.DelaySeconds(2);
                            }
                            _model.Cup.HowManyDice = 6;
                            _model.Cup.CanShowDice = false;
                            SaveRoot.GameStatus = EnumGameStatusList.RollDice;
                        }
                    }
                }
                else
                {
                    SaveRoot!.DiceScore = nums;
                    SaveRoot.GameStatus = EnumGameStatusList.ChooseDice;
                }
            }
            await ContinueTurnAsync();
        }
        private void TransferScore()
        {
            SingleInfo!.CurrentScore += SaveRoot!.TempScore;
            SaveRoot.TempScore = 0;
        }
        public int CalculateScore()
        {
            var tempList = _model.Cup!.DiceList.GetSelectedItems();
            return CalculateScore(tempList, false, out _);
        }
        public async Task AddToTempAsync(int score)
        {
            SaveRoot!.DiceScore = 0;
            SaveRoot.TempScore += score;
            _model!.Cup!.RemoveSelectedDice();
            var thisCard = _model.Pile1!.GetCardInfo();
            if (SaveRoot.FillsRequired > 0 || thisCard.Status == EnumCardStatusList.MustBust)
            {
                SaveRoot.GameStatus = EnumGameStatusList.RollDice;
            }
            else
            {
                SaveRoot.GameStatus = EnumGameStatusList.ChooseRoll;
            }
            await ContinueTurnAsync();
        }
        private int CalculateScore(CustomBasicList<SimpleDice> thisCol, bool considerAll, out bool hasFill)
        {
            hasFill = false;
            if (thisCol.Any(Items => Items.Index == 0))
            {
                throw new BasicBlankException("Cannot have a 0 for number");
            }
            if (thisCol.Count == 6 && considerAll == true)
            {
                if (thisCol.HasDuplicates(Items => Items.Value) == false)
                {
                    hasFill = true;
                    return 1500;
                }
            }
            int output;
            output = 0;
            int Nums = 0;
            bool rets;
            int x;
            for (x = 1; x <= 2; x++)
            {
                if (thisCol.Count < 3)
                {
                    break;
                }
                rets = HasTriple(ref thisCol, ref Nums);
                if (rets == true)
                {
                    if (Nums == 1)
                    {
                        output += 1000;
                    }
                    else if (Nums == 2)
                    {
                        output += 200;
                    }
                    else if (Nums == 3)
                    {
                        output += 300;
                    }
                    else if (Nums == 4)
                    {
                        output += 400;
                    }
                    else if (Nums == 5)
                    {
                        output += 500;
                    }
                    else if (Nums == 6)
                    {
                        output += 600;
                    }
                    else
                    {
                        throw new BasicBlankException("Cannot find the score for " + Nums);
                    }
                }
                else
                {
                    break;// if the first had no triple; then the second won't either
                }
            }
            if (thisCol.Count == 0)
            {
                if (considerAll == true)
                {
                    hasFill = true;
                }
                return output;
            }
            rets = HasSpecific(thisCol, 1, out int Manys);
            if (rets == true)
            {
                output += (Manys * 100);
            }
            if (thisCol.Count == 0)
            {
                if (considerAll == true)
                {
                    hasFill = true;
                }
                return output;
            }
            rets = HasSpecific(thisCol, 5, out Manys);
            if (rets == true)
            {
                output += (Manys * 50);
            }
            if (thisCol.Count == 0)
            {
                if (considerAll == true)
                {
                    hasFill = true;
                }
                return output;
            }
            if (considerAll == false)
            {
                return 0;
            }
            return output;
        }
        private CustomBasicList<int> WinList()
        {
            CustomBasicList<FillOrBustPlayerItem> tempList = PlayerList.OrderByDescending(items => items.TotalScore).ToCustomBasicList();
            if (tempList.First().TotalScore > tempList[1].TotalScore)
            {
                return new CustomBasicList<int> { tempList.First().Id };
            }
            int highScore = tempList.First().TotalScore;
            return tempList.Where(Items => Items.TotalScore == highScore).Select(items => items.Id).ToCustomBasicList();
        }
        private bool CanPlayRevenge()
        {
            var thisList = WinList();
            if (thisList.Any(items => items == WhoTurn))
            {
                return false;
            }
            return true;
        }
        private bool HasSpecific(CustomBasicList<SimpleDice> thisCol, int whichOne, out int howMany)
        {
            howMany = thisCol.Count(items => items.Value == whichOne);
            if (howMany == 0)
            {
                return false;
            }
            thisCol.RemoveAllOnly(items => items.Value == whichOne);
            return true;
        }
        private bool HasTriple(ref CustomBasicList<SimpleDice> thisCol, ref int number)
        {
            int x;
            int howMany;
            int y;
            int lists;
            lists = thisCol.Count;
            CustomBasicList<SimpleDice> newList = new CustomBasicList<SimpleDice>();
            for (x = 1; x <= 6; x++)
            {
                howMany = thisCol.Count(Items => Items.Value == x);
                if (howMany >= 3)
                {
                    number = x;
                    newList = thisCol.Where(Items => Items.Value == x).ToCustomBasicList();
                    for (y = 0; y <= 2; y++)
                    {
                        thisCol.RemoveSpecificItem(newList[y]);
                    }
                    if (thisCol.Count == 0 & lists > 3)
                    {
                        throw new BasicBlankException("There must be at least one item yet for triples");
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}