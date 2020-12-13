using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using GolfCardGameCP.Data;
using System.Linq;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings;
namespace GolfCardGameCP.Logic
{
    [SingletonGame]
    public class GolfCardGameMainGameClass : CardGameClass<RegularSimpleCard, GolfCardGamePlayerItem, GolfCardGameSaveInfo>, IMiscDataNM, IStartNewGame
    {
        private readonly GolfCardGameVMData _model;
        private readonly CommandContainer _command;
        private readonly GolfCardGameGameContainer _gameContainer;
        private readonly IBeginningProcesses _processes;
        public GolfCardGameMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            GolfCardGameVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<RegularSimpleCard> cardInfo,
            CommandContainer command,
            GolfCardGameGameContainer gameContainer,
            IBeginningProcesses processes
            )
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
            _gameContainer = gameContainer;
            _processes = processes;
            _gameContainer.ChangeHandAsync = ChangeHandAsync;
            _gameContainer.ChangeUnknownAsync = ChangeUnknownAsync;
        }
        internal async Task ChangeHandAsync(int whichOne)
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (SingleInfo.CanSendMessage(BasicData!) == true)
            {
                await Network!.SendAllAsync("changehand", whichOne);
            }
            var thisCard = SingleInfo.MainHandList[whichOne];
            var newCard = _model!.OtherPile!.GetCardInfo();
            var tempList = SingleInfo.MainHandList.ToRegularDeckDict();
            if (whichOne == 0)
            {
                tempList.RemoveFirstItem();
                tempList.InsertBeginning(newCard);
            }
            else
            {
                tempList.RemoveLastItem();
                tempList.Add(newCard);
            }
            SingleInfo.MainHandList.ReplaceRange(tempList);
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
            {
                _model.GolfHand1!.ChangeCard(whichOne, newCard);
                _model.OtherPile.AddCard(thisCard);
            }
            await ContinueTurnAsync();
        }
        internal async Task ChangeUnknownAsync(int whichOne)
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (SingleInfo.CanSendMessage(BasicData!) == true)
            {
                await Network!.SendAllAsync("changeunknown", whichOne);
            }
            var thisCard = SingleInfo.TempSets[whichOne];
            var newCard = _model!.OtherPile!.GetCardInfo();
            var tempList = SingleInfo.TempSets.ToRegularDeckDict();
            if (whichOne == 0)
            {
                tempList.RemoveFirstItem();
                tempList.InsertBeginning(newCard);
                SingleInfo.FirstChanged = true;
            }
            else
            {
                tempList.RemoveLastItem();
                tempList.Add(newCard);
                SingleInfo.SecondChanged = true;
            }
            SingleInfo.TempSets.ReplaceRange(tempList);
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
            {
                _model.HiddenCards1!.ChangeCard(whichOne, newCard);
            }
            thisCard.IsUnknown = false;
            await DiscardAsync(thisCard);
        }
        internal async Task KnockAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.OtherHuman)
            {
                ToastPlatform.ShowInfo($"{SingleInfo.NickName} has knocked.  Therefore; all players gets one more turn");
            }
            SingleInfo.Knocked = true;
            SaveRoot!.GameStatus = EnumStatusType.Knocked;
            await EndTurnAsync();
        }
        public override Task FinishGetSavedAsync()
        {
            if (SaveRoot!.GameStatus != EnumStatusType.Beginning)
                throw new BasicBlankException("No autoresume.  Therefore, had to be beginning");
            LoadControls();
            SaveRoot.LoadMod(_model);
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
            LoadControls();
            _model.Beginnings1.ObjectList.Clear();
            _model.GolfHand1.ObjectList.Clear();
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.FinishedChoosing = false;
                thisPlayer.Knocked = false;
                thisPlayer.FirstChanged = false;
                thisPlayer.SecondChanged = false;
            });
            SaveRoot!.Round++;
            SaveRoot.GameStatus = EnumStatusType.Beginning;
            SaveRoot.LoadMod(_model);
            return base.StartSetUpAsync(isBeginning);
        }
        protected override Task LastPartOfSetUpBeforeBindingsAsync()
        {
            SingleInfo = PlayerList!.GetSelf();
            if (SaveRoot!.GameStatus != EnumStatusType.Beginning)
            {
                throw new BasicBlankException("Must beginnings at first");
            }
            return base.LastPartOfSetUpBeforeBindingsAsync();
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "knock":
                    await KnockAsync();
                    break;
                case "selectbeginning":
                    SendBeginning thisB = await js.DeserializeObjectAsync<SendBeginning>(content);
                    await _processes.SelectBeginningAsync(thisB.Player, thisB.SelectList, thisB.UnsSelectList);
                    break;
                case "changeunknown":
                    await ChangeUnknownAsync(int.Parse(content));
                    break;
                case "changehand":
                    await ChangeHandAsync(int.Parse(content));
                    break;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            await base.StartNewTurnAsync();
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            SingleInfo.MainHandList.UnhighlightObjects();
            _command.ManuelFinish = true; //somebody else's turn.
            WhoTurn = await PlayerList.CalculateWhoTurnAsync();
            SingleInfo = PlayerList.GetWhoPlayer();
            if (SingleInfo.Knocked)
            {
                await EndRoundAsync();
                return;
            }
            await StartNewTurnAsync();
        }
        public override async Task ContinueTurnAsync()
        {
            if (SaveRoot!.GameStatus == EnumStatusType.Beginning)
            {
                SingleInfo = PlayerList!.GetSelf();
                _model!.NormalTurn = "None";
                _model.Instructions = "Choose the 2 cards to put into your hand";
                _model.Beginnings1!.ClearBoard(SingleInfo.MainHandList);
                if (BasicData!.MultiPlayer)
                {
                    Check!.IsEnabled = false; //just in case.
                }
                await ShowHumanCanPlayAsync();
                return;
            }
            SingleInfo = PlayerList!.GetWhoPlayer();
            if (SaveRoot.GameStatus == EnumStatusType.Knocked)
            {
                _model!.Instructions = "Take your last turn because a player knocked";
            }
            else
            {
                _model!.Instructions = "Take turn";
            }
            await base.ContinueTurnAsync();
        }
        private int WhoKnocked
        {
            get
            {
                if (SaveRoot!.GameStatus == EnumStatusType.Normal)
                {
                    return 0;
                }
                GolfCardGamePlayerItem thisPlayer = PlayerList.FirstOrDefault(items => items.Knocked);
                if (thisPlayer == null)
                {
                    return 0;
                }
                return thisPlayer.Id;
            }
        }
        private int WonRound(CustomBasicList<int> thisCol)
        {
            int leasts = 1000;
            int wins = 0;
            for (int x = 1; x <= thisCol.Count; x++)
            {
                int temps = thisCol[x - 1];
                if (temps == leasts)
                {
                    wins = 0;
                }
                else if (temps < leasts)
                {
                    leasts = temps;
                    wins = x;
                }
            }
            return wins;
        }
        private int ScoreCard(RegularSimpleCard thisCard)
        {
            if (thisCard.CardType == EnumRegularCardTypeList.Joker || thisCard.Value == EnumRegularCardValueList.Jack)
            {
                return 0;
            }
            if (thisCard.Value == EnumRegularCardValueList.Queen || thisCard.Value == EnumRegularCardValueList.King)
            {
                return 10;
            }
            return (int)thisCard.Value;
        }
        private int ScorePlayer()
        {
            int nums = SingleInfo!.MainHandList.Sum(items => ScoreCard(items));
            int temps = SingleInfo.TempSets.Sum(items => ScoreCard(items));
            return nums + temps;
        }
        private CustomBasicList<int> ListScores()
        {
            CustomBasicList<int> output = new CustomBasicList<int>();
            PlayerList!.ForEach(thisPlayer =>
            {
                SingleInfo = thisPlayer;
                output.Add(ScorePlayer());
            });
            return output;
        }
        private void Scoring(int knocks, int wins, CustomBasicList<int> scoreList)
        {
            int scores;
            for (int x = 1; x <= PlayerList.Count(); x++)
            {
                if (knocks == 0)
                {
                    scores = scoreList[x - 1];
                }
                else if (knocks > 0 && wins == knocks && knocks == x)
                {
                    scores = 0;
                }
                else if (knocks > 0 && wins != knocks && knocks == x)
                {
                    scores = scoreList[x - 1];
                    scores *= 2;
                }
                else
                {
                    scores = scoreList[x - 1];
                }
                var thisPlayer = PlayerList![x];
                thisPlayer.PreviousScore = scores;
                thisPlayer.TotalScore += scores;
            }
        }
        protected override async Task AnimatePickupAsync(RegularSimpleCard card)
        {
            await Aggregator.AnimatePickUpDiscardAsync(card);
        }
        public override async Task EndRoundAsync()
        {
            int knocks = WhoKnocked;
            var scoreList = ListScores();
            int wins = WonRound(scoreList);
            Scoring(knocks, wins, scoreList);
            _model!.HiddenCards1!.RevealCards();
            if (SaveRoot!.Round == 9)
            {
                SingleInfo = PlayerList.OrderBy(items => items.TotalScore).First();
                _model.Instructions = "Game Over";
                await ShowWinAsync();
                return;
            }
            await this.RoundOverNextAsync();
        }
        Task IStartNewGame.ResetAsync()
        {
            SaveRoot!.Round = 0;
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.PreviousScore = 0;
                thisPlayer.TotalScore = 0;
            });
            return Task.CompletedTask;
        }
    }
}