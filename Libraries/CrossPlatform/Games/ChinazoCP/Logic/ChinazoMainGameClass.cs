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
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using ChinazoCP.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings;
namespace ChinazoCP.Logic
{
    [SingletonGame]
    public class ChinazoMainGameClass : CardGameClass<ChinazoCard, ChinazoPlayerItem, ChinazoSaveInfo>, IMiscDataNM, IStartNewGame
    {
        private readonly ChinazoVMData _model;
        private readonly CommandContainer _command;
        private readonly ChinazoGameContainer _gameContainer;
        public ChinazoMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            ChinazoVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<ChinazoCard> cardInfo,
            CommandContainer command,
            ChinazoGameContainer gameContainer,
            ChinazoDelegates delegates)
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
            _gameContainer = gameContainer;
            _rummys = new RummyProcesses<EnumSuitList, EnumRegularColorList, ChinazoCard>();
            delegates.CardsToPassOut = (() => CardsToPassOut);
            _gameContainer.ModifyCards = ModifyCards;
        }
        internal CustomBasicList<SetList>? SetsList { get; set; }
        private readonly RummyProcesses<EnumSuitList, EnumRegularColorList, ChinazoCard> _rummys;
        public int OtherTurn
        {
            get
            {
                return SaveRoot!.PlayOrder.OtherTurn;
            }
            set
            {
                SaveRoot!.PlayOrder.OtherTurn = value;
            }

        }
        internal int CardsToPassOut
        {
            get
            {
                if (SaveRoot!.Round < 4)
                {
                    return 8;
                }
                if (SaveRoot.Round <= 7)
                {
                    return 11;
                }
                return 13;
            }
        }
        public void ModifyCards(CustomBasicList<ChinazoCard> thisList)
        {
            thisList.ForEach(thisCard =>
            {
                if (thisCard.Value == EnumRegularCardValueList.Joker)
                {
                    thisCard.Points = 50;
                }
                else if (thisCard.Value == EnumRegularCardValueList.LowAce || thisCard.Value == EnumRegularCardValueList.HighAce)
                {
                    thisCard.Points = 20;
                }
                else if (thisCard.Value >= EnumRegularCardValueList.Nine)
                {
                    thisCard.Points = 10;
                }
                else
                {
                    thisCard.Points = (int)thisCard.Value;
                }
            });
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            int x = SaveRoot!.SetList.Count;
            x.Times(items =>
            {
                PhaseSet thisSet = new PhaseSet(_gameContainer!);
                _model!.MainSets.CreateNewSet(thisSet);
            });
            PlayerList!.ForEach(thisPlayer =>
            {
                if (thisPlayer.AdditionalCards.Count > 0)
                {
                    thisPlayer.MainHandList.AddRange(thisPlayer.AdditionalCards);
                    thisPlayer.AdditionalCards.Clear();
                }
            });
            SingleInfo = PlayerList.GetSelf();
            SortCards();
            _model!.MainSets.LoadSets(SaveRoot.SetList);
            return base.FinishGetSavedAsync();
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            CreateSets();
            IsLoaded = true;
        }
        private void CreateSets()
        {
            _rummys.HasSecond = true;
            _rummys.HasWild = true;
            _rummys.LowNumber = 1;
            _rummys.HighNumber = 14;
            SetsList = new CustomBasicList<SetList>();
            SetList FirstSet = new SetList(); //for now, just keep the names since i copied/pasted.
            FirstSet.Description = "1 Set of 3, 1 Run of 3";
            FillInSets(FirstSet, 1, EnumRummyType.Sets);
            FillInSets(FirstSet, 1, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "2 Sets of 3";
            FillInSets(FirstSet, 2, EnumRummyType.Sets);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "2 Runs of 3";
            FillInSets(FirstSet, 2, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "2 Sets of 3, 1 Run Of 3";
            FillInSets(FirstSet, 2, EnumRummyType.Sets);
            FillInSets(FirstSet, 1, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "1 Set Of 3, 2 Runs of 3";
            FillInSets(FirstSet, 1, EnumRummyType.Sets);
            FillInSets(FirstSet, 2, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "3 Sets Of 3";
            FillInSets(FirstSet, 3, EnumRummyType.Sets);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "3 Runs Of 3";
            FillInSets(FirstSet, 3, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "3 Sets of 3, 1 Run Of 3";
            FillInSets(FirstSet, 3, EnumRummyType.Sets);
            FillInSets(FirstSet, 1, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "1 Set Of 3, 3 Runs of 3";
            FillInSets(FirstSet, 1, EnumRummyType.Sets);
            FillInSets(FirstSet, 3, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "4 Sets Of 3";
            FillInSets(FirstSet, 4, EnumRummyType.Sets);
            SetsList.Add(FirstSet);
            FirstSet = new SetList();
            FirstSet.Description = "4 Runs Of 3";
            FillInSets(FirstSet, 4, EnumRummyType.Runs);
            SetsList.Add(FirstSet);
            if (SetsList.Count != 11)
            {
                throw new Exception("Must have 11 sets created");
            }
        }
        private void FillInSets(SetList firstSet, int howMany, EnumRummyType whatSet)
        {
            int x;
            var loopTo = howMany;
            for (x = 1; x <= loopTo; x++)
            {
                firstSet.PhaseSets.Add(new SetInfo() { WhatSet = whatSet, HowMany = 3 });
            }
        }
        protected override async Task ComputerTurnAsync()
        {
            if (OtherTurn > 0)
            {
                await PassAsync();
                return;
            }
            await DiscardAsync(SingleInfo!.MainHandList.GetRandomItem(true).Deck); //hopefully is not forced to remove that card from hand (?)

        }
        public override Task PopulateSaveRootAsync()
        {
            SaveRoot!.SetList = _model!.MainSets!.SavedSets();
            ChinazoPlayerItem self = PlayerList!.GetSelf();
            self.AdditionalCards = _model.TempSets!.ListAllObjects();
            return base.PopulateSaveRootAsync();
        }
        protected override Task StartSetUpAsync(bool isBeginning)
        {
            SaveRoot!.Round++;
            if (Test!.DoubleCheck == true && SaveRoot.Round < 3)
            {
                SaveRoot.Round = 3;
            }
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.LaidDown = false;
            });
            SaveRoot.ImmediatelyStartTurn = true;
            SaveRoot.HadChinazo = false;
            return base.StartSetUpAsync(isBeginning);
        }
        protected override Task LastPartOfSetUpBeforeBindingsAsync()
        {
            if (IsLoaded == false)
            {
                LoadControls();
            }
            SingleInfo = PlayerList!.GetSelf();
            _model!.MainSets!.ClearBoard();
            SaveRoot!.SetList.Clear();
            return base.LastPartOfSetUpBeforeBindingsAsync();
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "laiddowninitial":
                    await CreateSetsAsync(content);
                    await LaidDownInitialSetsAsync();
                    return;
                case "pass":
                    await PassAsync();
                    return;
                case "expandrummy":
                    SendExpandedSet thiss = await js.DeserializeObjectAsync<SendExpandedSet>(content);
                    await AddToSetAsync(thiss.Number, thiss.Deck, thiss.Position);
                    return;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            await base.StartNewTurnAsync();
            OtherTurn = WhoTurn;
            await ContinueTurnAsync();
        }
        public override async Task EndTurnAsync()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
            UnselectCards();
            _command.ManuelFinish = true; //because it could be somebody else's turn.
            WhoTurn = await PlayerList.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        private void UnselectCards()
        {
            if (SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
                return;
            }
            _model!.PlayerHand1!.EndTurn();
            _model.TempSets!.EndTurn();
        }
        public override async Task ContinueTurnAsync()
        {
            if (OtherTurn > 0)
            {
                SingleInfo = PlayerList!.GetOtherPlayer();
                _model!.OtherLabel = SingleInfo.NickName;
            }
            else
            {
                SingleInfo = PlayerList!.GetWhoPlayer();
                _model!.OtherLabel = "None";
            }
            _model.PhaseData = SetsList![SaveRoot!.Round - 1].Description;
            await base.ContinueTurnAsync();
        }
        private void RemoveCard(int deck)
        {
            if (SingleInfo!.PlayerCategory == EnumPlayerCategory.Computer)
            {
                return; //i think computer player would have already removed their card.
            }
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
            {
                if (_model!.TempSets!.HasObject(deck))
                {
                    _model.TempSets.RemoveObject(deck);
                    return;
                }
            }
            SingleInfo.MainHandList.RemoveObjectByDeck(deck);
        }
        public override async Task DiscardAsync(ChinazoCard ThisCard)
        {
            RemoveCard(ThisCard.Deck);
            await AnimatePlayAsync(ThisCard);
            if (SingleInfo!.ObjectCount == 0) //going to try to trust the object count.
            {
                await EndRoundAsync();
                return;
            }
            await EndTurnAsync();
        }
        public async Task PassAsync()
        {
            OtherTurn = await PlayerList!.CalculateOtherTurnAsync(false);
            if (OtherTurn == 0)
            {
                SingleInfo = PlayerList.GetWhoPlayer();
                await DrawAsync();
                return;
            }
            SingleInfo = PlayerList.GetOtherPlayer();
            await ProcessOtherTurnAsync();
        }
        private async Task ProcessOtherTurnAsync()
        {
            if (SingleInfo!.LaidDown == true)
            {
                await PassAsync(); //they have to pass automatically because they they laid down. or you discarded.
                return;
            }
            await ContinueTurnAsync();
        }
        protected override void GetPlayerToContinueTurn()
        {
            if (OtherTurn == 0)
            {
                base.GetPlayerToContinueTurn();
                return;
            }
            SingleInfo = PlayerList!.GetOtherPlayer();
        }
        private void ResetSuccess()
        {
            SetsList!.ForEach(thisPhase =>
            {
                thisPhase.PhaseSets.ForEach(ThisSet => ThisSet.DidSucceed = false);
            });
        }
        protected override Task PlayerChosenForPickingUpFromDiscardAsync()
        {
            SingleInfo = PlayerList!.GetOtherPlayer();
            return Task.CompletedTask;
        }
        protected override async Task AfterPickupFromDiscardAsync()
        {
            if (OtherTurn == 0)
            {
                await base.AfterDrawingAsync();
                return;
            }
            if (WhoTurn != OtherTurn)
            {
                OtherTurn = 0;
                PlayerDraws = WhoTurn;
                await DrawAsync();
                return;
            }
            OtherTurn = 0;
            await base.AfterDrawingAsync();
        }
        private async Task CreateSetsAsync(string Message)
        {
            var firstTemp = await js.DeserializeObjectAsync<CustomBasicList<string>>(Message);
            foreach (var thisFirst in firstTemp)
            {
                var thisSend = await js.DeserializeObjectAsync<SendNewSet>(thisFirst);
                var thisCol = await thisSend.CardListData.GetObjectsFromDataAsync(SingleInfo!.MainHandList!);
                TempInfo thisTemp = new TempInfo();
                thisTemp.CardList = thisCol;
                thisTemp.WhatSet = thisSend.WhatSet;
                thisTemp.UseSecond = thisSend.UseSecond;
                CreateNewSet(thisTemp);
            }
        }
        private int CalculatePoints(ChinazoPlayerItem thisPlayer)
        {
            int points = thisPlayer.MainHandList.Sum(items => items.Points);
            if (SaveRoot!.HadChinazo == true)
            {
                points *= 2;
            }
            return points;
        }

        public override async Task EndRoundAsync()
        {
            UnselectCards();
            SingleInfo = PlayerList!.GetSelf();
            var tempCol = _model!.TempSets!.ListObjectsRemoved();
            SingleInfo.MainHandList.AddRange(tempCol);
            SortCards();
            PlayerList.ForEach(tempPlayer =>
            {
                ModifyCards(tempPlayer.MainHandList);
                int points = CalculatePoints(tempPlayer);
                tempPlayer.CurrentScore = points;
                tempPlayer.TotalScore += points;
            });
            if (SaveRoot!.Round == 11)
            {
                SingleInfo = PlayerList.OrderBy(items => items.TotalScore).First();
                await ShowWinAsync();
                return;
            }
            await this.RoundOverNextAsync();
        }
        public async Task LayDownOtherSetsAsync()
        {
            _model!.MainSets!.SetList.ForEach(thisSet =>
            {
                thisSet.CheckList();
            });
            int manys = SingleInfo!.ObjectCount;
            if (manys == 0)
            {
                await EndRoundAsync();
                return;
            }
            await ContinueTurnAsync();
        }
        public async Task LaidDownInitialSetsAsync()
        {
            bool didHave = PlayerList.Any(items => items.LaidDown == true);
            SingleInfo = PlayerList!.GetWhoPlayer();
            SingleInfo.LaidDown = true;
            if (didHave == false)
            {
                int manys = SingleInfo.ObjectCount;
                if (manys <= 1)
                {
                    SaveRoot!.HadChinazo = true;
                    if (SingleInfo.PlayerCategory != EnumPlayerCategory.Self)
                    {
                        ToastPlatform.ShowInfo($"{SingleInfo.NickName} had a chinzao.  Therefore; all other players will be getting double points");
                    }
                }
            }
            await LayDownOtherSetsAsync();
        }
        public void CreateNewSet(TempInfo thisTemp)
        {
            PhaseSet thisSet = new PhaseSet(_gameContainer);
            thisSet.CreateSet(thisTemp.CardList, thisTemp.WhatSet, thisTemp.UseSecond);
            _model!.MainSets!.CreateNewSet(thisSet);
        }
        public async Task AddToSetAsync(int ourSet, int deck, int position)
        {
            PhaseSet thisSet = _model!.MainSets!.GetIndividualSet(ourSet);
            var thisCard = _gameContainer.DeckList!.GetSpecificItem(deck);
            thisSet.AddCard(thisCard, position);
            RemoveCard(deck);
            await LayDownOtherSetsAsync();
        }
        public bool CanAddToSet(PhaseSet thisSet, out ChinazoCard? whatCard, int section, out string message)
        {
            message = "";
            whatCard = null;
            int howManySelected = _model!.PlayerHand1!.HowManySelectedObjects + _model.TempSets!.HowManySelectedObjects;
            if (howManySelected == 0)
            {
                return false;
            }
            if (howManySelected > 1)
            {
                return false;
            }
            int decks;
            if (_model.TempSets.HowManySelectedObjects == 1)
            {
                int piles = _model.TempSets.PileForSelectedObject;
                decks = _model.TempSets.DeckForSelectedObjected(piles);
            }
            else
            {
                decks = _model.PlayerHand1.ObjectSelected();
            }
            whatCard = _gameContainer.DeckList!.GetSpecificItem(decks);
            int position = thisSet.PositionToPlay(whatCard, section);
            if (position == 0)
            {
                message = "Sorry, this card cannot be used to expand the set"; //i guess this would be okay no matter what.
                return false;
            }
            return true;
        }
        public IDeckDict<ChinazoCard> WhatSet(int whichOne)
        {
            return _model!.TempSets!.ObjectList(whichOne);
        }
        public CustomBasicList<TempInfo> ListValidSets()
        {
            CustomBasicList<TempInfo> output = new CustomBasicList<TempInfo>();
            SetList thisSet = SetsList![SaveRoot!.Round - 1];
            DeckRegularDict<ChinazoCard> thisCollection;
            IDeckDict<ChinazoCard> tempCollection;
            TempInfo thisTemp;
            for (int x = 1; x <= _model!.TempSets!.HowManySets; x++)
            {
                tempCollection = WhatSet(x);
                thisCollection = new DeckRegularDict<ChinazoCard>();
                if (tempCollection.Count > 0)
                {
                    thisCollection.AddRange(tempCollection);
                }
                if (thisCollection.Count(items => items.IsObjectWild == false) >= 2)
                {

                    foreach (var newSet in thisSet.PhaseSets)
                    {
                        if (newSet.DidSucceed == false)
                        {
                            newSet.DidSucceed = _rummys!.IsNewRummy(thisCollection, newSet.HowMany, newSet.WhatSet);
                            if (newSet.DidSucceed == true)
                            {
                                thisTemp = new TempInfo();
                                thisTemp.CardList = thisCollection;
                                thisTemp.WhatSet = newSet.WhatSet;
                                thisTemp.UseSecond = _rummys.UseSecond;
                                output.Add(thisTemp);
                                _model.TempSets.ClearBoard(x);
                                break;
                            }
                        }
                    }
                }
                if (output.Count == thisSet.PhaseSets.Count)
                {
                    break; //to keep you from doing more than you can.
                }
            }
            ResetSuccess();
            return output;
        }
        public bool CanLayDownInitialSets()
        {
            SetList thisSet = SetsList![SaveRoot!.Round - 1];
            DeckRegularDict<ChinazoCard> thisCollection;
            IDeckDict<ChinazoCard> tempCollection;
            for (int x = 1; x <= _model!.TempSets!.HowManySets; x++)
            {
                tempCollection = WhatSet(x);
                thisCollection = new DeckRegularDict<ChinazoCard>();
                if (tempCollection.Count > 0)
                {
                    thisCollection.AddRange(tempCollection);
                }
                if (thisCollection.Count(items => items.IsObjectWild == false) >= 2)
                {
                    foreach (var newSet in thisSet.PhaseSets)
                    {
                        if (newSet.DidSucceed == false)
                        {
                            newSet.DidSucceed = _rummys!.IsNewRummy(thisCollection, newSet.HowMany, newSet.WhatSet);
                            if (newSet.DidSucceed == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            bool rets = thisSet.PhaseSets.All(items => items.DidSucceed == true);
            ResetSuccess();
            return rets;
        }
        Task IStartNewGame.ResetAsync()
        {
            SaveRoot!.Round = 0;
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.CurrentScore = 0;
                thisPlayer.TotalScore = 0;
            });
            return Task.CompletedTask;
        }
    }
}