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
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using ConcentrationCP.Data;
using System.Linq;
using System.Threading.Tasks;
namespace ConcentrationCP.Logic
{
    [SingletonGame]
    public class ConcentrationMainGameClass : CardGameClass<RegularSimpleCard, ConcentrationPlayerItem, ConcentrationSaveInfo>, IMiscDataNM
    {
        private readonly ConcentrationVMData _model;
        private readonly CommandContainer _command;
        public ConcentrationMainGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            ConcentrationVMData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<RegularSimpleCard> cardInfo,
            CommandContainer command,
            ConcentrationGameContainer gameContainer)
            : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            _model!.GameBoard1!.PileList = SaveRoot!.BoardList.ToCustomBasicList();
            return base.FinishGetSavedAsync();
        }
        public override async Task ContinueTurnAsync()
        {
            var thisCol = _model!.GameBoard1!.GetSelectedCards();
            if (thisCol.Count == 2)
            {
                await SaveStateAsync(); //so you can't cheat.
                await ProcessPlayAsync(thisCol);
                return;
            }
            await base.ContinueTurnAsync();
        }
        private async Task ProcessPlayAsync(DeckRegularDict<RegularSimpleCard> thisCol)
        {
            if (IsValidMove(thisCol) == true)
            {
                _command.UpdateAll();
                if (Test!.NoAnimations == false)
                {
                    await Delay!.DelaySeconds(1);
                }
                RemoveComputer(thisCol);
                _model!.GameBoard1!.SelectedCardsGone();
                SingleInfo!.Pairs++;
                if (_model.GameBoard1.CardsGone == true || Test.ImmediatelyEndGame == true)
                {
                    await GameOverAsync();
                    return;
                }
                await ContinueTurnAsync();
                return;
            }
            _command.UpdateAll();
            if (Test!.NoAnimations == false)
            {
                await Delay!.DelaySeconds(5);
            }
            _model!.GameBoard1!.UnselectCards();
            AddComputer(thisCol);
            await EndTurnAsync();
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            IsLoaded = true; 
        }
        private bool IsValidMove(DeckRegularDict<RegularSimpleCard> thisCol)
        {
            if (Test!.AllowAnyMove == true && SingleInfo!.PlayerCategory == EnumPlayerCategory.Self)
            {
                return true; //for testing.
            }
            return thisCol.HasDuplicates(items => items.Value);
        }
        private void RemoveComputer(DeckRegularDict<RegularSimpleCard> ThisCol)
        {
            if (PlayerList.Any(items => items.PlayerCategory == EnumPlayerCategory.Computer) == false)
            {
                return;
            }
            ThisCol.ForEach(thisCard =>
            {
                if (SaveRoot!.ComputerList.ObjectExist(thisCard.Deck) == true)
                {
                    SaveRoot.ComputerList.RemoveObjectByDeck(thisCard.Deck);
                }
            });
        }
        private void AddComputer(DeckRegularDict<RegularSimpleCard> thisCol)
        {
            if (PlayerList.Any(items => items.PlayerCategory == EnumPlayerCategory.Computer) == false)
            {
                return;
            }
            thisCol.ForEach(thisCard =>
            {
                if (SaveRoot!.ComputerList.ObjectExist(thisCard.Deck) == false)
                {
                    SaveRoot.ComputerList.Add(thisCard);
                }
            });
        }
        protected override async Task ComputerTurnAsync()
        {
            if (Test!.NoAnimations == false)
            {
                await Delay!.DelaySeconds(1);
            }
            await SelectCardAsync(ComputerAI.CardToTry(this, _model));
        }
        protected override Task StartSetUpAsync(bool isBeginning)
        {
            LoadControls();
            PlayerList!.ForEach(thisPlayer => thisPlayer.Pairs = 0);
            SaveRoot!.ComputerList = new DeckRegularDict<RegularSimpleCard>();
            _model!.GameBoard1!.ClearBoard();
            return base.StartSetUpAsync(isBeginning);
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "selectcard":
                    await SelectCardAsync(int.Parse(content));
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
            _command.ManuelFinish = true;
            WhoTurn = await PlayerList.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        private async Task GameOverAsync()
        {
            SingleInfo = PlayerList.OrderByDescending(items => items.Pairs).First();
            await ShowWinAsync();
        }
        public override async Task PopulateSaveRootAsync()
        {
            SaveRoot!.BoardList = _model.GameBoard1.PileList!.ToCustomBasicList();
            await base.PopulateSaveRootAsync();
        }
        internal async Task SelectCardAsync(int deck)
        {
            if (SingleInfo!.CanSendMessage(BasicData!) == true)
            {
                await Network!.SendAllAsync("selectcard", deck);
            }
            _model!.GameBoard1!.SelectCard(deck);
            await ContinueTurnAsync();
        }
    }
}