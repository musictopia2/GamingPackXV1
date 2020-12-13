using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Extensions; //most likely will be used.
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using RackoCP.Cards;
using RackoCP.Data;
using System.Linq;
using System.Threading.Tasks;
namespace RackoCP.Logic
{
    [SingletonGame]
    public class RackoMainGameClass : CardGameClass<RackoCardInformation, RackoPlayerItem, RackoSaveInfo>, IMiscDataNM, IStartNewGame
    {
        private readonly RackoVMData _model;
        private readonly CommandContainer _command;
        private readonly RackoGameContainer _gameContainer;
        private int _previousUse; //for computer ai.
        public RackoMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            RackoVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<RackoCardInformation> cardInfo,
            CommandContainer command,
            RackoGameContainer gameContainer,
            RackoDelegates delegates)
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
            _gameContainer = gameContainer;
            delegates.PlayerCount = (() => SaveRoot.PlayerList.Count);
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            PlayerList!.ForEach(thisPlayer =>
            {
                if (thisPlayer.PlayerCategory == EnumPlayerCategory.Self)
                {
                    thisPlayer.CanShowValues = true;
                }
                else
                {
                    thisPlayer.CanShowValues = false;
                }
            });
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
            if (Test!.NoAnimations == false)
            {
                await Delay!.DelaySeconds(.5);
            }
            if (HasRacko() == true)
            {
                if (BasicData!.MultiPlayer == true)
                {
                    await Network!.SendAllAsync("racko");
                }
                await EndRoundAsync();
                return;
            }
            int nums;
            if (_gameContainer.AlreadyDrew == false)
            {
                nums = ComputerAI.PickUp(this, _model);
                if (nums > 0)
                {
                    _previousUse = nums;
                    await PickupFromDiscardAsync();
                    return;
                }
                await DrawAsync();
                return;
            }
            RackoCardInformation thisCard;
            if (_previousUse > 0)
            {
                thisCard = SingleInfo!.MainHandList[_previousUse - 1];
                thisCard.WillKeep = true;
                if (BasicData!.MultiPlayer == true)
                {
                    await _gameContainer.SendDiscardMessageAsync(thisCard.Deck);
                }
                await DiscardAsync(thisCard.Deck);
                return;
            }
            nums = ComputerAI.CardToPlay(this, _model);
            if (nums > 0)
            {
                thisCard = SingleInfo!.MainHandList[nums - 1];
                thisCard.WillKeep = true;
                if (BasicData!.MultiPlayer == true)
                {
                    await _gameContainer.SendDiscardMessageAsync(thisCard.Deck);
                }
                await DiscardAsync(thisCard.Deck);
                return;
            }
            if (BasicData!.MultiPlayer == true)
            {
                await Network!.SendAllAsync("discardcurrent");
            }
            await DiscardCurrentAsync();
        }
        protected override Task StartSetUpAsync(bool isBeginning)
        {
            LoadControls();
            PlayerList!.ForEach(thisPlayer =>
            {
                if (thisPlayer.PlayerCategory == EnumPlayerCategory.Self)
                {
                    thisPlayer.CanShowValues = true;
                }
                else
                {
                    thisPlayer.CanShowValues = false;
                }
            });
            return base.StartSetUpAsync(isBeginning);
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "racko":
                    await EndRoundAsync();
                    return;
                case "discardcurrent":
                    await DiscardCurrentAsync();
                    return;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            await base.StartNewTurnAsync();
            _previousUse = 0;
            _gameContainer.AlreadyDrew = false;
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            SingleInfo.MainHandList.UnhighlightObjects();
            _command.ManuelFinish = true; //because it could be somebody else's turn.
            WhoTurn = await PlayerList.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        public override async Task EndRoundAsync()
        {
            PlayerList!.ForEach(thisPlayer => thisPlayer.CanShowValues = true);
            PlayerList.ForEach(thisPlayer =>
            {
                thisPlayer.ScoreRound = WhatScore(thisPlayer);
                if (thisPlayer.ScoreRound == 0)
                {
                    throw new BasicBlankException("Cannot be 0 points");
                }
                thisPlayer.TotalScore += thisPlayer.ScoreRound;
            });
            PossibleWinProcess();
            if (SingleInfo != null)
            {
                await ShowWinAsync();
                return;
            }
            await this.RoundOverNextAsync();
        }
        private void PossibleWinProcess()
        {
            SingleInfo = PlayerList.Where(items => items.TotalScore >= 300).FirstOrDefault();
        }
        private int HowManyHigher(RackoPlayerItem thisPlayer)
        {
            int x = 0;
            int thisNum = 0;
            foreach (var thisCard in thisPlayer.MainHandList)
            {
                if (thisCard.Value > thisNum)
                {
                    x++;
                    thisNum = thisCard.Value;
                }
                else
                {
                    return x;
                }
            }
            return x;
        }
        private int WhatScore(RackoPlayerItem thisPlayer)
        {
            if (thisPlayer.Id != WhoTurn)
            {
                return HowManyHigher(thisPlayer) * 5;
            }
            return 75 + BonusPoints(thisPlayer);
        }
        private int BonusPoints(RackoPlayerItem thisPlayer)
        {
            RummyProcesses<EnumSuitList, EnumSuitList, RackoCardInformation> thisInfo = new RummyProcesses<EnumSuitList, EnumSuitList, RackoCardInformation>();
            thisInfo.HasSecond = false;
            thisInfo.HasWild = false;
            thisInfo.NeedMatch = false; //try this too.
            thisInfo.LowNumber = 1;
            RackoDeckCount temps = MainContainer.Resolve<RackoDeckCount>();
            thisInfo.HighNumber = temps.GetDeckCount();
            DeckRegularDict<RackoCardInformation> tempCol = thisPlayer.MainHandList.ToRegularDeckDict();
            var newColls = thisInfo.WhatNewRummy(tempCol, 6, EnumRummyType.Runs, true);
            if (newColls.Count > 0)
            {
                return 400;
            }
            newColls = thisInfo.WhatNewRummy(tempCol, 5, EnumRummyType.Runs, true);
            if (newColls.Count > 0)
            {
                return 200;
            }
            newColls = thisInfo.WhatNewRummy(tempCol, 4, EnumRummyType.Runs, true);
            if (newColls.Count > 0)
            {
                return 100;
            }
            newColls = thisInfo.WhatNewRummy(tempCol, 3, EnumRummyType.Runs, true);
            if (newColls.Count > 0)
            {
                return 50;
            }
            return 0;
        }
        public override async Task DiscardAsync(RackoCardInformation thisCard)
        {
            var newCard = _model!.OtherPile!.GetCardInfo();
            newCard.WillKeep = thisCard.WillKeep;
            if (thisCard.Deck == newCard.Deck)
            {
                throw new BasicBlankException("Cannot be duplicate for discard.  Rethink");
            }
            if (SingleInfo!.MainHandList.Contains(thisCard) == true)
            {
                thisCard = SingleInfo.MainHandList.GetSpecificItem(thisCard.Deck);
            }
            else
            {
                if (SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
                {
                    ToastPlatform.ShowError("Card not found.  Rethinking may be required.  Not sure though");
                }
                await ContinueTurnAsync();
                return;
            }
            SingleInfo.MainHandList.ReplaceItem(thisCard, newCard);
            if (SingleInfo.MainHandList.ObjectExist(thisCard.Deck))
            {
                throw new BasicBlankException("Failed To Replace Card");
            }
            _model.OtherPile.ClearCards();
            await AnimatePlayAsync(thisCard);
            await EndTurnAsync();
        }
        public bool HasRacko()
        {
            int thisNum = 0;
            foreach (var thisCard in SingleInfo!.MainHandList)
            {
                if (thisCard.Value < thisNum)
                {
                    return false;
                }
                thisNum = thisCard.Value;
            }
            return true;
        }
        public async Task DiscardCurrentAsync()
        {
            var thisCard = _model!.OtherPile!.GetCardInfo();
            _model.OtherPile.ClearCards();
            await AnimatePlayAsync(thisCard);
            await EndTurnAsync();
        }
        Task IStartNewGame.ResetAsync()
        {
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.ScoreRound = 0;
                thisPlayer.TotalScore = 0;
            });
            return Task.CompletedTask;
        }
        public override Task ContinueTurnAsync()
        {
            PlayerList.ForEach(player => player.UpdateAllValues());
            return base.ContinueTurnAsync();
        }
    }
}