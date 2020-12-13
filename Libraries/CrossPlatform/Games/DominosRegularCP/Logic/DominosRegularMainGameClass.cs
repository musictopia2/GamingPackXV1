using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using DominosRegularCP.Data;
using System.Linq;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace DominosRegularCP.Logic
{
    [SingletonGame]
    public class DominosRegularMainGameClass : DominosGameClass<SimpleDominoInfo, DominosRegularPlayerItem, DominosRegularSaveInfo>, IMiscDataNM
    {
        public DominosRegularMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            DominosRegularVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            BasicGameContainer<DominosRegularPlayerItem, DominosRegularSaveInfo> gameContainer
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, gameContainer)
        {
            _model = model;
            DominosToPassOut = 6;
            _model.GameBoard1.DominoPileClicked = DominoPileClicked;
        }
        private readonly DominosRegularVMData _model;
        private bool _didPlay;
        private bool _wentOut;
        private async Task DominoPileClicked(int whichOne)
        {
            int decks = _model.PlayerHand1!.ObjectSelected();
            if (decks == 0)
            {
                ToastPlatform.ShowError($"Sorry, you have to select a domino to play for {whichOne}");
                return;
            }
            var thisDomino = SingleInfo!.MainHandList.GetSpecificItem(decks);
            if (_model.GameBoard1.IsValidMove(whichOne, thisDomino) == false)
            {
                ToastPlatform.ShowError("Illegal Move");
                return;
            }
            await PlayDominoAsync(decks, whichOne);
        }
        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            ProtectedLoadBone();
            _model.GameBoard1!.LoadSavedGame(SaveRoot);
            AfterPassedDominos(); //i did need this too.
            return Task.CompletedTask;
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            LoadUpDominos();
            IsLoaded = true; //i think needs to be here.
        }
        protected override async Task ComputerTurnAsync()
        {
            if (Test!.NoAnimations == false)
            {
                await Delay!.DelaySeconds(1);
            }
            int decks = ComputerAI.DominoToPlay(out int whichOne, this, _model.GameBoard1);
            if (decks > 0)
            {
                await PlayDominoAsync(decks, whichOne);
                return;
            }
            if (_model.BoneYard!.HasBone() == false || _model.BoneYard.HasDrawn())
            {
                _wentOut = false;
                if (SingleInfo!.CanSendMessage(BasicData!))
                {
                    await Network!.SendEndTurnAsync();
                }
                await EndTurnAsync();
                return;
            }
            await DrawDominoAsync(_model.BoneYard.DrawDomino());
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            SaveRoot!.Beginnings = true;
            ClearBoneYard();
            PassDominos();
            _model.GameBoard1!.ClearBoard(SaveRoot);
            PlayerList!.ForEach(thisPlayer =>
            {
                thisPlayer.TotalScore = 0;
                thisPlayer.NoPlay = false;
            });
            var (turn, dominoused) = PrivateWhoStartsFirst();
            WhoTurn = turn;
            SingleInfo = PlayerList.GetWhoPlayer();
            SingleInfo.MainHandList.RemoveObjectByDeck(dominoused.Deck);
            WhoTurn = await PlayerList.CalculateWhoTurnAsync();
            _model.GameBoard1.PopulateCenter(dominoused);
            await FinishUpAsync(isBeginning);
        }
        public override async Task StartNewTurnAsync()
        {
            SaveRoot!.Beginnings = false; //forgot this part.
            ProtectedStartTurn();
            _didPlay = false;
            await ContinueTurnAsync(); //most of the time, continue turn.  can change to what is needed
        }
        public override async Task ContinueTurnAsync()
        {
            if (SaveRoot!.Beginnings)
            {
                await StartNewTurnAsync();
                return;
            }
            await base.ContinueTurnAsync();
            _model.GameBoard1.ReportCanExecuteChange();
        }
        public override Task PopulateSaveRootAsync() //usually needs this too.
        {
            ProtectedSaveBone();
            return Task.CompletedTask;
        }
        async Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status)
            {
                case "play":
                    PlayInfo thisPlay = await js.DeserializeObjectAsync<PlayInfo>(content);
                    await PlayDominoAsync(thisPlay.Deck, thisPlay.WhichOne);
                    return;
                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public async Task PlayDominoAsync(int deck, int whichOne)
        {
            if (SingleInfo!.CanSendMessage(BasicData!))
            {
                PlayInfo thisPlay = new PlayInfo();
                thisPlay.Deck = deck;
                thisPlay.WhichOne = whichOne;
                await Network!.SendAllAsync("play", thisPlay);
            }
            SimpleDominoInfo thisDomino = SingleInfo!.MainHandList.GetSpecificItem(deck);
            await PlayDominoAsync(thisDomino, whichOne);
        }
        public override async Task EndTurnAsync()
        {
            if (_didPlay == false && _model.BoneYard!.HasDrawn() == false)
            {
                SingleInfo!.NoPlay = true;
            }
            else
            {
                SingleInfo!.NoPlay = false;
            }
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
            {
                _model.PlayerHand1!.EndTurn();
            }
            if (PlayerList.All(items => items.NoPlay))
            {
                _wentOut = false;
                await GameOverAsync();
                return;
            }
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        private async Task GameOverAsync()
        {
            Scoring();
            if (_wentOut == false)
            {
                SingleInfo = PlayerList.OrderBy(items => items.TotalScore).First();
            }
            await ShowWinAsync();
        }
        public async Task PlayDominoAsync(SimpleDominoInfo thisDomino, int whichOne)
        {
            SingleInfo!.MainHandList.RemoveObjectByDeck(thisDomino.Deck);
            _didPlay = true;
            _model.GameBoard1!.MakeMove(whichOne, thisDomino);
            if (SingleInfo.MainHandList.Count == 0)
            {
                _wentOut = true;
                await GameOverAsync();
                return;
            }
            await EndTurnAsync();
        }
        public override Task PlayDominoAsync(int deck)
        {
            throw new BasicBlankException("This game has an exception.  Must use the one with 2 parameters unfortunately");
        }
        private void Scoring()
        {
            PlayerList!.ForEach(thisPlayer => thisPlayer.TotalScore = thisPlayer.MainHandList.Sum(items => items.Points));
        }
        private (int turn, SimpleDominoInfo dominoused) PrivateWhoStartsFirst()
        {
            var thisInfo = GetHighestDouble();
            if (thisInfo.turn > 0)
            {
                return thisInfo;
            }
            return GetMostPoints();
        }

        private (int turn, SimpleDominoInfo dominoused) GetHighestDouble()
        {
            int highs = -1;
            SimpleDominoInfo? highDomino = null;
            int x = 0;
            int whichs = 0;
            int currents;
            PlayerList!.ForEach(thisPlayer =>
            {
                x++;
                thisPlayer.MainHandList.ForConditionalItems(items => items.FirstNum == items.SecondNum, thisDomino =>
                {
                    currents = thisDomino.FirstNum;
                    if (currents > highs)
                    {
                        highs = currents;
                        whichs = x;
                        highDomino = thisDomino;
                    }
                });
            });
            return (whichs!, highDomino!);
        }
        private (int turn, SimpleDominoInfo dominoused) GetMostPoints()
        {
            int highs = -1;
            SimpleDominoInfo? highDomino = null;
            int x = 0;
            int whichs = 0;
            int currents;
            PlayerList!.ForEach(thisPlayer =>
            {
                x++;
                thisPlayer.MainHandList.ForEach(thisDomino =>
                {
                    currents = thisDomino.FirstNum + thisDomino.SecondNum;
                    if (currents > highs)
                    {
                        highs = currents;
                        whichs = x;
                        highDomino = thisDomino;
                    }
                });
            });
            return (whichs!, highDomino!);
        }
    }
}