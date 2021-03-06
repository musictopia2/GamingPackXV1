using A8RoundRummyCP.Cards;
using A8RoundRummyCP.Data;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings;
namespace A8RoundRummyCP.Logic
{
    [SingletonGame]
    public class A8RoundRummyMainGameClass : CardGameClass<A8RoundRummyCardInformation, A8RoundRummyPlayerItem, A8RoundRummySaveInfo>, IMiscDataNM
    {
        private readonly A8RoundRummyVMData _model;
        private readonly CommandContainer _command;
        private readonly A8RoundRummyGameContainer _gameContainer;
        internal A8RoundRummyCardInformation? LastCard { get; set; }
        internal bool LastSuccessful { get; set; }
        public bool WasGuarantee { get; set; }
        public A8RoundRummyCardInformation? CardForDiscard { get; set; }
        public EnumPlayerStatus PlayerStatus { get; set; }
        private bool _wasNew;
        public A8RoundRummyMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            A8RoundRummyVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<A8RoundRummyCardInformation> cardInfo,
            CommandContainer command,
            A8RoundRummyGameContainer gameContainer)
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
            _gameContainer = gameContainer;
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            _wasNew = false;
            CardForDiscard = null;
            WasGuarantee = false;
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
            PlayerList!.ForEach(thisPlayer => thisPlayer.TotalScore = 0);
            _wasNew = false;
            WasGuarantee = false;
            CardForDiscard = null;
            CustomBasicList<RoundClass> tempList = new CustomBasicList<RoundClass>();
            RoundClass thisRound = new RoundClass();
            thisRound.Description = "Round 1:  Same Color";
            thisRound.Category = EnumCategory.Colors;
            thisRound.Rummy = EnumRummyType.Regular;
            thisRound.Points = 1;
            tempList.Add(thisRound);
            thisRound = new RoundClass();
            thisRound.Description = "Round 2:  Same Shape";
            thisRound.Category = EnumCategory.Shapes;
            thisRound.Rummy = EnumRummyType.Regular;
            thisRound.Points = 2;
            tempList.Add(thisRound);
            thisRound = new RoundClass();
            thisRound.Description = "Round 3:  Same Shape And Color";
            thisRound.Category = EnumCategory.Both;
            thisRound.Rummy = EnumRummyType.Regular;
            thisRound.Points = 3;
            tempList.Add(thisRound);
            thisRound = new RoundClass();
            thisRound.Description = "Round 4:  Straight";
            thisRound.Category = EnumCategory.None;
            thisRound.Rummy = EnumRummyType.Straight;
            thisRound.Points = 4;
            tempList.Add(thisRound);
            thisRound = new RoundClass();
            thisRound.Description = "Round 5:  Straight Same Color";
            thisRound.Category = EnumCategory.Colors;
            thisRound.Rummy = EnumRummyType.Straight;
            thisRound.Points = 5;
            tempList.Add(thisRound);
            thisRound = new RoundClass();
            thisRound.Description = "Round 6:  Straight Same Shape";
            thisRound.Category = EnumCategory.Shapes;
            thisRound.Rummy = EnumRummyType.Straight;
            thisRound.Points = 6;
            tempList.Add(thisRound);
            thisRound = new RoundClass();
            thisRound.Description = "Round 7:  Straight Same Shape And Color";
            thisRound.Category = EnumCategory.Both;
            thisRound.Rummy = EnumRummyType.Straight;
            thisRound.Points = 7;
            tempList.Add(thisRound);
            thisRound = new RoundClass();
            thisRound.Description = "Round 8:  Same Number";
            thisRound.Category = EnumCategory.None;
            thisRound.Rummy = EnumRummyType.Kinds;
            thisRound.Points = 8;
            tempList.Add(thisRound);
            if (Test!.DoubleCheck)
            {
                tempList.KeepConditionalItems(items => items.Points >= 4);
            }
            SaveRoot!.RoundList.ReplaceRange(tempList);
            return base.StartSetUpAsync(isBeginning);
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "goout":
                    MultiplayerOut thisOut = await js.DeserializeObjectAsync<MultiplayerOut>(content);
                    WasGuarantee = thisOut.WasGuaranteed;
                    CardForDiscard = _gameContainer.DeckList!.GetSpecificItem(thisOut.Deck);
                    await GoOutAsync();
                    break;
                case "reversecards":
                    var list = await PopulateCardsFromTurnHandAsync(content);
                    await DiscardAllCardsAsync(list);
                    break;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            await base.StartNewTurnAsync();
            await ShowNextTurnAsync(); //i think here too.
            PlayerDraws = 0;
            if (PlayerStatus != EnumPlayerStatus.Regular)
            {
                PlayerStatus = EnumPlayerStatus.Regular;
                LeftToDraw = 0;
                await DrawAsync();
                return;
            }
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            SingleInfo.MainHandList.UnhighlightObjects();
            _wasNew = false;
            _command.ManuelFinish = true; //because it could be somebody else's turn.
            WhoTurn = await PlayerList.CalculateWhoTurnAsync(true);
            await StartNewTurnAsync();
        }
        public override async Task ContinueTurnAsync()
        {
            await ShowNextTurnAsync();
            await base.ContinueTurnAsync();
        }
        private async Task ShowNextTurnAsync()
        {
            A8RoundRummyPlayerItem newPlayer;
            int newTurn = await PlayerList!.CalculateWhoTurnAsync();
            newPlayer = PlayerList[newTurn];
            _model!.NextTurn = newPlayer.NickName;
        }
        protected override bool ShowNewCardDrawn(A8RoundRummyPlayerItem tempPlayer)
        {
            if (_wasNew == true)
            {
                return false;
            }
            return base.ShowNewCardDrawn(tempPlayer);
        }
        public bool CanProcessDiscard(out bool pickUp, out int deck, out string message)
        {
            message = "";
            deck = 0;
            if (_gameContainer.AlreadyDrew == false && _gameContainer.PreviousCard == 0)
            {
                pickUp = true;
            }
            else
            {
                pickUp = false;
            }
            int Selected = _model!.PlayerHand1!.ObjectSelected();
            if (pickUp == true)
            {
                if (Selected > 0)
                {
                    message = "There is at least one card selected.  Unselect all the cards before picking up from discard";
                    return false;
                }
                return true;
            }
            if (Selected == 0)
            {
                message = "Sorry, you must select a card to discard";
                return false;
            }
            if (Selected == _gameContainer.PreviousCard)
            {
                deck = 0;
                message = "Sorry, you cannot discard the one that was picked up since there is more than one card to choose from";
                return false;
            }
            deck = Selected;
            return true;
        }
        public bool CanGoOut()
        {
            WasGuarantee = SingleInfo!.MainHandList.GuaranteedVictory(this);
            if (WasGuarantee == true)
            {
                CardForDiscard = LastCard;
                return true;
            }
            if (SingleInfo.MainHandList.HasRummy(this) == false)
            {
                return false;
            }
            CardForDiscard = LastCard;
            return true;
        }
        private async Task GameOverAsync()
        {
            SaveRoot!.RoundList.Clear();
            await ShowWinAsync();
        }
        public async Task GoOutAsync()
        {
            if (SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
                ToastPlatform.ShowInfo($"{SingleInfo.NickName} went out");
            }
            else
            {
                _model!.PlayerHand1!.EndTurn();
            }
            var thisList = SingleInfo.MainHandList.ToRegularDeckDict();
            thisList.RemoveObjectByDeck(CardForDiscard!.Deck);
            await thisList.ForEachAsync(async thisCard =>
            {
                thisCard.Drew = false;
                thisCard.IsSelected = false;
                SingleInfo.MainHandList.RemoveObjectByDeck(thisCard.Deck);
                await AnimatePlayAsync(thisCard);
                if (Test!.NoAnimations == false)
                {
                    await Delay!.DelaySeconds(.25);
                }
            });
            SingleInfo.MainHandList.RemoveObjectByDeck(CardForDiscard.Deck);
            await AnimatePlayAsync(CardForDiscard);
            if (WasGuarantee == true)
            {
                await GameOverAsync();
                return;
            }
            SingleInfo.TotalScore += SaveRoot!.RoundList.First().Points;
            if (SaveRoot.RoundList.Count == 1)
            {
                SingleInfo = PlayerList.OrderByDescending(Items => Items.TotalScore).First();
                await GameOverAsync();
                return;
            }
            _wasNew = true;
            PlayerStatus = EnumPlayerStatus.WentOut;
            if (SingleInfo.MainHandList.Count > 0)
            {
                throw new BasicBlankException("Can't have any cards.  Maybe trying clearing to double check but not sure");
            }
            LeftToDraw = 7;
            await DrawAsync();
        }
        protected override async Task AfterDrawingAsync()
        {
            _wasNew = false;
            if (SingleInfo!.MainHandList.Count > 8)
            {
                throw new BasicBlankException("Can't have more than 8 cards in hand");
            }
            if (PlayerStatus == EnumPlayerStatus.WentOut)
            {
                PlayerStatus = EnumPlayerStatus.Regular;
                SaveRoot!.RoundList.RemoveFirstItem();
                await EndTurnAsync();
                return;
            }
            if (PlayerStatus == EnumPlayerStatus.Reversed)
            {
                SaveRoot!.PlayOrder.IsReversed = !SaveRoot.PlayOrder.IsReversed;
                if (SaveRoot.PlayOrder.OtherTurn == 0)
                {
                    throw new BasicBlankException("Otherturn must be filled in");
                }
                WhoTurn = SaveRoot.PlayOrder.OtherTurn;
                SaveRoot.PlayOrder.OtherTurn = 0;
                PlayerStatus = EnumPlayerStatus.Regular;
                await StartNewTurnAsync();
                return;
            }
            _gameContainer.AlreadyDrew = true;
            await base.AfterDrawingAsync();
        }
        public override async Task DiscardAsync(A8RoundRummyCardInformation thisCard)
        {
            if (SingleInfo!.PlayerCategory == EnumPlayerCategory.Self)
                _model!.PlayerHand1!.EndTurn();
            if (thisCard.CardType == EnumCardType.Reverse)
            {
                thisCard.Drew = false;
                thisCard.IsSelected = false;
                if (SingleInfo.PlayerCategory != EnumPlayerCategory.Computer)
                {
                    SingleInfo.MainHandList.RemoveObjectByDeck(thisCard.Deck);
                }
                bool ShowMessageBox;
                await AnimatePlayAsync(thisCard);
                ShowMessageBox = SingleInfo.PlayerCategory != EnumPlayerCategory.Self;
                SaveRoot!.PlayOrder.OtherTurn = WhoTurn; //after being reversed then the current player will get another turn.
                PlayerStatus = EnumPlayerStatus.Reversed;
                WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
                SingleInfo = PlayerList.GetWhoPlayer();
                if (ShowMessageBox == true)
                {
                    ToastPlatform.ShowWarning($"{SingleInfo.NickName} has to discard all the cards and draw new cards");
                }
                if (SingleInfo.PlayerCategory == EnumPlayerCategory.OtherHuman)
                {
                    Check!.IsEnabled = true;
                    return; //waiting for message from other player.
                }
                if (SingleInfo.PlayerCategory == EnumPlayerCategory.Computer && BasicData.MultiPlayer)
                {
                    throw new BasicBlankException("This game should not have had any extra computer players");
                }
                var list = SingleInfo.MainHandList.GetRandomList().ToRegularDeckDict(); //hopefully gets full list
                if (list.Count != SingleInfo.MainHandList.Count)
                {
                    throw new BasicBlankException("After getting random cards, does not reconcile the count.  This means missing cards");
                }
                if (BasicData.MultiPlayer)
                {
                    await Network!.SendCustomDeckListAsync("reversecards", list);
                }
                await DiscardAllCardsAsync(list);
                return;
            }
            if (SingleInfo.PlayerCategory != EnumPlayerCategory.Computer)
            {
                SingleInfo.MainHandList.RemoveObjectByDeck(thisCard.Deck);
                if (SingleInfo.MainHandList.Count == 8)
                {
                    throw new BasicBlankException("Failed To Discard");
                }
            }
            await AnimatePlayAsync(thisCard);
            await EndTurnAsync();
        }
        private async Task DiscardAllCardsAsync(DeckRegularDict<A8RoundRummyCardInformation> cards)
        {
            //the player whose turn it is has to already know what cards to send to other players to discard.
            await cards.ForEachAsync(async card =>
            {
                SingleInfo!.MainHandList.RemoveObjectByDeck(card.Deck);
                await AnimatePlayAsync(card);
            });
            LeftToDraw = 7;
            _wasNew = true;
            PlayerDraws = WhoTurn;
            await DrawAsync();
        }

    }
}