using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicDrawables.MiscClasses;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using SorryCP.Data;
using System.Threading.Tasks;
namespace SorryCP.Logic
{
    [SingletonGame]
    public class SorryMainGameClass
        : SimpleBoardGameClass<SorryPlayerItem, SorrySaveInfo, EnumColorChoice, int>
    {
        public SorryMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            SorryVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            SorryGameContainer container,
            DrawShuffleClass<CardInfo, SorryPlayerItem> shuffle,
            GameBoardProcesses gameBoard
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, container)
        {
            _model = model;
            _command = command;
            _shuffle = shuffle;
            _gameBoard = gameBoard;
            _shuffle.AfterDrawingAsync = AfterDrawingAsync;
            _shuffle.CurrentPlayer = () => SingleInfo!;
            _shuffle.AfterFirstShuffle = TestCardOnTop;
        }
        private void TestCardOnTop(IListShuffler<CardInfo> deck)
        {
            if (Test!.ImmediatelyEndGame == false)
            {
                return;
            }
            deck.PutCardOnTop(1);
        }
        private readonly SorryVMData _model;
        private readonly CommandContainer _command;
        private readonly DrawShuffleClass<CardInfo, SorryPlayerItem> _shuffle;
        private readonly GameBoardProcesses _gameBoard;
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            BoardGameSaved();
            _gameBoard.LoadSavedGame();
            _shuffle.SaveRoot = SaveRoot;
            if (SaveRoot.OurColor == EnumColorChoice.None && PlayerList.DidChooseColors())
            {
                throw new BasicBlankException("Our color was not populated.");
            }
            HookMod();
            Aggregator.RepaintBoard();
            return Task.CompletedTask;
        }
        private void HookMod()
        {
            SaveRoot.LoadMod(_model);
            if (SaveRoot.DidDraw)
            {
                _model.CardDetails = SaveRoot.CurrentCard!.Details;
            }
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            _gameBoard.LoadBoard();
            IsLoaded = true;
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            EraseColors();
            _shuffle.SaveRoot = SaveRoot;
            HookMod();
            SaveRoot!.ImmediatelyStartTurn = true;
            await FinishUpAsync(isBeginning);
        }
        public override async Task AfterChoosingColorsAsync()
        {
            _gameBoard.ClearBoard();
            SaveRoot!.Instructions = "Waiting for cards to be shuffled";
            WhoTurn = WhoStarts;
            _command.ManuelFinish = true;
            if (BasicData!.MultiPlayer == false)
            {
                await _shuffle!.FirstShuffleAsync(false); //do not autodraw this time.  not candyland.
                await StartNewTurnAsync();
                return;
            }
            if (BasicData.Client)
            {
                Check!.IsEnabled = true;
                return;
            }
            await _shuffle!.FirstShuffleAsync(false);
            SaveRoot.DidDraw = false; //because you now did not draw.
            SaveRoot.Instructions = "None";
            SaveRoot.ImmediatelyStartTurn = true; //so the client will start new turn.
            _doContinue = false;
            await StartNewTurnAsync(); //so the client has the proper data first.  otherwise, it gets hosed for the client.
            _doContinue = true;
            await Network!.SendAllAsync("restoregame", SaveRoot);
            await StartNewTurnAsync();
        }
        private bool _doContinue = true;
        public override async Task StartNewTurnAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                PrepStartTurn();
                if (BasicData!.MultiPlayer == false)
                {
                    SaveRoot!.Instructions = $"{SingleInfo!.NickName} needs to draw a card";
                }
                else
                {
                    int ourID = PlayerList!.GetSelf().Id;
                    if (ourID == WhoTurn)
                    {
                        SaveRoot!.Instructions = "Draw a card";
                    }
                    else
                    {
                        SaveRoot!.Instructions = $"Waiting for {SingleInfo!.NickName} to draw a card";
                    }
                }
                _gameBoard.StartTurn();
            }
            if (_doContinue == false)
            {
                return;
            }
            await ContinueTurnAsync();
        }
        public override async Task ContinueTurnAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                if (Test!.DoubleCheck)
                {
                    Test.DoubleCheck = false;
                    await _gameBoard.GetValidMovesAsync();
                    return;
                }
            }
            await base.ContinueTurnAsync();
        }
        public override async Task MakeMoveAsync(int space)
        {
            await _gameBoard.MakeMoveAsync(space);
        }
        public override async Task EndTurnAsync()
        {
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            _command.ManuelFinish = true;
            await StartNewTurnAsync();
        }
        internal async Task DrawCardAsync()
        {
            await _shuffle!.DrawAsync();
        }
        private async Task AfterDrawingAsync()
        {
            _gameBoard.ShowDraw();
            await _gameBoard.GetValidMovesAsync();
        }
        protected override async Task ShowHumanCanPlayAsync()
        {
            await base.ShowHumanCanPlayAsync();
            _command.UpdateAll();
        }
    }
}